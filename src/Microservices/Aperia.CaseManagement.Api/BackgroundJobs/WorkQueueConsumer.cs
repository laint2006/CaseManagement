using Aperia.Core.Messaging.Models;
using Aperia.Core.Messaging.RabbitMq;
using Microsoft.Extensions.Options;

namespace Aperia.CaseManagement.Api.BackgroundJobs;

/// <summary>
/// The Work Queue Consumer
/// </summary>
/// <seealso cref="Aperia.Core.Messaging.RabbitMq.AsyncRabbitMqConsumer" />
public class WorkQueueConsumer : AsyncRabbitMqConsumer
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<WorkQueueConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkQueueConsumer" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="connectionManager">The connection manager.</param>
    /// <param name="consumerSettings">The consumer settings.</param>
    public WorkQueueConsumer(ILogger<WorkQueueConsumer> logger, IRabbitMqConnectionManager connectionManager, IOptions<RabbitMqConsumerSettings> consumerSettings)
        : base(logger, connectionManager, consumerSettings)
    {
        this._logger = logger;
    }

    /// <summary>
    /// Handles the message asynchronous.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    protected override Task HandleMessageAsync(Event @event, CancellationToken cancellationToken = default)
    {
        this._logger.LogInformation("No processor found for event {EventType}", @event.EventType);

        return Task.CompletedTask;
    }

}