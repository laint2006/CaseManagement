namespace Aperia.Core.Messaging.RabbitMq
{
    /// <summary>
    /// The Rabbit Mq Settings
    /// </summary>
    public class RabbitMqSettings
    {
        /// <summary>
        /// The configuration section
        /// </summary>
        public const string  ConfigurationSection = "RabbitMqSettings";

        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        public string HostName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the virtual host.
        /// </summary>
        public string? VirtualHost { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether [use asynchronous dispatch consumer].
        /// </summary>
        public bool UseAsyncDispatchConsumer { get; set; }

        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        public int RetryCount { get; set; }
    }
}