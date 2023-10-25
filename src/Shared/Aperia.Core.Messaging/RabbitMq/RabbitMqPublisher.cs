using Aperia.Core.Messaging.Models;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;

namespace Aperia.Core.Messaging.RabbitMq;

/// <summary>
/// The Rabbit Mq Publisher
/// </summary>
/// <seealso cref="Aperia.Core.Messaging.IEventPublisher" />
public class RabbitMqPublisher : IEventPublisher
{
    /// <summary>
    /// The connection manager
    /// </summary>
    private readonly IRabbitMqConnectionManager _connectionManager;

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<RabbitMqPublisher> _logger;

    /// <summary>
    /// The publisher settings
    /// </summary>
    private readonly RabbitMqPublisherSettings _publisherSettings;

    /// <summary>
    /// The json serializer options
    /// </summary>
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = false,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqPublisher" /> class.
    /// </summary>
    /// <param name="connectionManager">The connection manager.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="publisherSettings">The rabbit mq publisher settings.</param>
    public RabbitMqPublisher(IRabbitMqConnectionManager connectionManager, ILogger<RabbitMqPublisher> logger, IOptions<RabbitMqPublisherSettings> publisherSettings)
    {
        _connectionManager = connectionManager;
        _logger = logger;
        _publisherSettings = publisherSettings.Value;
        _serializerOptions.Converters.Add(new JsonStringEnumConverter());
    }

    /// <summary>
    /// Publishes the event asynchronous.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<ErrorOr<PublishResult>> PublishAsync(Event @event, CancellationToken cancellationToken = default)
    {
        if (!_connectionManager.IsConnected)
        {
            _connectionManager.TryConnect();
        }

        var policy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(this._publisherSettings.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s", @event.Id, $"{time.TotalSeconds:n1}");
            });

        using var channel = _connectionManager.CreateModel();
        this.EnsureExchangeIsExists(channel);
        var payload = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(payload);

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish(this._publisherSettings.ExchangeName, this._publisherSettings.RoutingKey, true, properties, body);
#if DEBUG
            _logger.LogInformation("Publishing event to RabbitMQ: {EventId}. Body: {Body}", @event.Id, Encoding.UTF8.GetString(body));
#endif
        });

        await Task.CompletedTask;

        return PublishResult.Published;
    }

    /// <summary>
    /// Ensures the channel is exists.
    /// </summary>
    /// <param name="channel">The channel.</param>
    private void EnsureExchangeIsExists(IModel channel)
    {
        channel.ExchangeDeclare(this._publisherSettings.ExchangeName, this._publisherSettings.ExchangeType);
        //channel.QueueDeclare(this._publisherSettings.QueueName, false, false, false, null);
        //channel.QueueBind(this._publisherSettings.QueueName, this._publisherSettings.ExchangeName, this._publisherSettings.RoutingKey, null);
    }

}