namespace Aperia.Core.Messaging.RabbitMq;

/// <summary>
/// The Rabbit Mq Publisher Settings
/// </summary>
public class RabbitMqPublisherSettings
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
    /// Gets or sets the retry count.
    /// </summary>
    public int RetryCount { get; set; } = 5;

}