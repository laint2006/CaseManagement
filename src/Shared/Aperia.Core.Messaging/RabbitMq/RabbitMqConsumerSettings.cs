namespace Aperia.Core.Messaging.RabbitMq
{
    /// <summary>
    /// The Rabbit Mq Consumer Settings
    /// </summary>
    public class RabbitMqConsumerSettings
    {
        /// <summary>
        /// Gets or sets the name of the exchange.
        /// </summary>
        public string ExchangeName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the exchange type.
        /// </summary>
        public string ExchangeType { get; set; } = RabbitMQ.Client.ExchangeType.Fanout;

        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        public string QueueName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the routing key.
        /// </summary>
        public string RoutingKey { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of dead letter exchange.
        /// </summary>
        public string? DeadLetterExchangeName { get; set; }

        /// <summary>
        /// Gets or sets the dead letter routing key.
        /// </summary>
        public string? DeadLetterRoutingKey { get; set; }

    }
}