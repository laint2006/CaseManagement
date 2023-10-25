using Aperia.Core.Application.Services;
using Aperia.Core.Messaging.Models;
using Aperia.Core.Messaging.RabbitMq;
using Aperia.SlaOla.Api.Commands.CalculateLa;
using Aperia.SlaOla.Api.Messaging.EventData;
using Aperia.SlaOla.Api.Models;
using Microsoft.Extensions.Options;

namespace Aperia.SlaOla.Api.BackgroundJobs
{
    /// <summary>
    /// The Work Queue Consumer
    /// </summary>
    /// <seealso cref="Aperia.Core.Messaging.RabbitMq.AsyncRabbitMqConsumer" />
    public class WorkQueueConsumer : AsyncRabbitMqConsumer
    {
        /// <summary>
        /// The service scope factory
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<WorkQueueConsumer> _logger;

        /// <summary>
        /// The json serializer
        /// </summary>
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkQueueConsumer" /> class.
        /// </summary>
        /// <param name="serviceScopeFactory">The service scope factory.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="connectionManager">The connection manager.</param>
        /// <param name="consumerSettings">The consumer settings.</param>
        /// <param name="jsonSerializer">The json serializer.</param>
        public WorkQueueConsumer(IServiceScopeFactory serviceScopeFactory, ILogger<WorkQueueConsumer> logger,
                                            IRabbitMqConnectionManager connectionManager, IOptions<RabbitMqConsumerSettings> consumerSettings, IJsonSerializer jsonSerializer)
            : base(logger, connectionManager, consumerSettings)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this._logger = logger;
            this._jsonSerializer = jsonSerializer;
        }

        /// <summary>
        /// Handles the message asynchronous.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected override async Task HandleMessageAsync(Event @event, CancellationToken cancellationToken = default)
        {
            var command = this.CalculateLaCommand(@event);
            if (command is null)
            {
                this._logger.LogInformation("Can not process the unknown event of type {EventType}", @event.EventType);
                return;
            }

            var scope = this._serviceScopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<IMediator>();
            await sender.Send(command, cancellationToken);

            this._logger.LogInformation("Processed event: {EventId} of type {EventType}", @event.Id, @event.EventType);
        }

        /// <summary>
        /// Calculates the la command.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <returns></returns>
        private CalculateLaCommand? CalculateLaCommand(Event @event)
        {
            if (!"Inquiry.Created".Equals(@event.EventType, StringComparison.OrdinalIgnoreCase) )
            {
                return null;
            }

            var inquiry = this._jsonSerializer.Deserialize<Inquiry>(this._jsonSerializer.Serialize(@event.Payload));
            if (inquiry is null)
            {
                return null;
            }

            var histories = new List<ChangeHistory>
            {
                new()
                {
                    Attribute = "OwnerType",
                    Value = inquiry.OwnerType
                },
                new()
                {
                    Attribute = "OwnerId",
                    Value = inquiry.OwnerId?.ToString()
                },
                new()
                {
                    Attribute = "Status",
                    Value = inquiry.Status
                },
                new()
                {
                    Attribute = "SecondaryStatus",
                    Value = inquiry.SecondaryStatus
                },
                new()
                {
                    Attribute = "CreatedDate",
                    Value = inquiry.CreatedDate?.ToString("yyyy-MM-dd HH:mm:ss")
                }
            };

            return new CalculateLaCommand(@event.Source, inquiry.Id, histories);
        }

    }
}