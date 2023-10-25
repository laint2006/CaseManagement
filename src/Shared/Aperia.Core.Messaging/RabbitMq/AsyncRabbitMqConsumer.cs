using Aperia.Core.Messaging.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text;

namespace Aperia.Core.Messaging.RabbitMq;

/// <summary>
/// The Asynchronous Rabbit Mq Consumer
/// </summary>
/// <seealso cref="Microsoft.Extensions.Hosting.IHostedService" />
public abstract class AsyncRabbitMqConsumer : IHostedService
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<AsyncRabbitMqConsumer> _logger;

    /// <summary>
    /// The connection manager
    /// </summary>
    private readonly IRabbitMqConnectionManager _connectionManager;

    /// <summary>
    /// The event queue settings
    /// </summary>
    private readonly RabbitMqConsumerSettings _consumerSettings;

    /// <summary>
    /// The consumer channel
    /// </summary>
    private IModel? _consumerChannel;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncRabbitMqConsumer" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="connectionManager">The connection manager.</param>
    /// <param name="consumerSettings">The event queue settings.</param>
    protected AsyncRabbitMqConsumer(ILogger<AsyncRabbitMqConsumer> logger, IRabbitMqConnectionManager connectionManager, IOptions<RabbitMqConsumerSettings> consumerSettings)
    {
        this._logger = logger;
        this._connectionManager = connectionManager;
        this._consumerSettings = consumerSettings.Value;
    }

    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Starting {ServiceName}", this.GetType().Name);

        if (!this._connectionManager.IsConnected)
        {
            this._connectionManager.TryConnect();
        }

        this._consumerChannel = this.CreateConsumerChannel();
        this.StartBasicAsyncConsume();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Stopping {ServiceName}", this.GetType().Name);

        this._consumerChannel?.Dispose();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Ensures the queue is exists.
    /// </summary>
    /// <param name="channel">The channel.</param>
    private void EnsureQueueIsExists(IModel channel)
    {
        var parameters = new Dictionary<string, object>();
        if (!string.IsNullOrWhiteSpace(this._consumerSettings.DeadLetterExchangeName))
        {
            parameters.Add("x-dead-letter-exchange", this._consumerSettings.DeadLetterExchangeName);
        }

        if (!string.IsNullOrWhiteSpace(this._consumerSettings.DeadLetterRoutingKey))
        {
            parameters.Add("x-dead-letter-routing-key", this._consumerSettings.DeadLetterRoutingKey);
        }

        channel.ExchangeDeclare(this._consumerSettings.ExchangeName, this._consumerSettings.ExchangeType, true);
        channel.QueueDeclare(this._consumerSettings.QueueName, false, false, false, parameters.Count == 0 ? null : parameters);
        channel.QueueBind(this._consumerSettings.QueueName, this._consumerSettings.ExchangeName, this._consumerSettings.RoutingKey);
    }

    /// <summary>
    /// Creates the consumer channel.
    /// </summary>
    /// <returns></returns>
    private IModel CreateConsumerChannel()
    {
        if (!this._connectionManager.IsConnected)
        {
            _connectionManager.TryConnect();
        }

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = _connectionManager.CreateModel();
        this.EnsureQueueIsExists(channel);

        channel.CallbackException += (_, ea) =>
        {
            _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

            _consumerChannel?.Dispose();
            _consumerChannel = CreateConsumerChannel();

            this.StartBasicAsyncConsume();
        };

        return channel;
    }

    /// <summary>
    /// Starts the basic asynchronous consume.
    /// </summary>
    private void StartBasicAsyncConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if (_consumerChannel == null)
        {
            _logger.LogError("StartBasicAsyncConsume can't call on _consumerChannel == null");

            return;
        }

        var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
        consumer.Received += OnReceivedMessage;

        _consumerChannel.BasicConsume(this._consumerSettings.QueueName, false, consumer);
    }

    /// <summary>
    /// Handles the Received event of the Consumer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="eventArgs">The <see cref="BasicDeliverEventArgs"/> instance containing the event data.</param>
    private async Task OnReceivedMessage(object sender, BasicDeliverEventArgs eventArgs)
    {
        string? message = null;
        try
        {
            message = Encoding.UTF8.GetString(eventArgs.Body.Span);
            var @event = JsonSerializer.Deserialize<Event>(message);
            if (@event is null)
            {
                _logger.LogInformation("Unable to process message: {Message}", message);
                return;
            }

            await this.HandleMessageAsync(@event);

            _consumerChannel?.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);

            _consumerChannel?.BasicNack(eventArgs.DeliveryTag, false, false);
        }
    }

    /// <summary>
    /// Handles the message asynchronous.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    protected abstract Task HandleMessageAsync(Event @event, CancellationToken cancellationToken = default);

}