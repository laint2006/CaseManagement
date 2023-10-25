namespace Aperia.Core.Messaging.RabbitMq
{
    /// <summary>
    /// The Rabbit Mq Connection Manager
    /// </summary>
    /// <seealso cref="Aperia.Core.Messaging.RabbitMq.IRabbitMqConnectionManager" />
    public class RabbitMqConnectionManager : IRabbitMqConnectionManager
    {
        /// <summary>
        /// The connection factory
        /// </summary>
        private readonly IConnectionFactory _connectionFactory;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<RabbitMqConnectionManager> _logger;

        /// <summary>
        /// The synchronize root
        /// </summary>
        private readonly object _syncRoot = new();

        /// <summary>
        /// The retry count
        /// </summary>
        private readonly int _retryCount;

        /// <summary>
        /// The connection
        /// </summary>
        private IConnection? _connection;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqConnectionManager"/> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="retryCount">The retry count.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connectionFactory
        /// or
        /// logger
        /// </exception>
        public RabbitMqConnectionManager(IConnectionFactory connectionFactory, ILogger<RabbitMqConnectionManager> logger, int retryCount = 5)
        {
            this._connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._retryCount = retryCount;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        public bool IsConnected => !this._disposed && _connection is { IsOpen: true };

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">No RabbitMQ connections are available to perform this action</exception>
        public IModel CreateModel()
        {
            if (!this.IsConnected || this._connection is null)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return this._connection.CreateModel();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
            if (this._connection is null)
            {
                return;
            }

            try
            {
                this._connection.ConnectionShutdown -= OnConnectionShutdown;
                this._connection.CallbackException -= OnCallbackException;
                this._connection.ConnectionBlocked -= OnConnectionBlocked;
                this._connection.Dispose();
            }
            catch (IOException ex)
            {
                this._logger.LogCritical(ex.ToString());
            }
        }

        /// <summary>
        /// Tries the connect.
        /// </summary>
        /// <returns></returns>
        public bool TryConnect()
        {
            this._logger.LogInformation("RabbitMQ Client is trying to connect");

            lock (_syncRoot)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        this._logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s", $"{time.TotalSeconds:n1}");
                    });

                policy.Execute(() =>
                {
                    this._connection = _connectionFactory.CreateConnection();
                });

                if (this.IsConnected && this._connection is not null)
                {
                    this._connection.ConnectionShutdown += OnConnectionShutdown;
                    this._connection.CallbackException += OnCallbackException;
                    this._connection.ConnectionBlocked += OnConnectionBlocked;

                    this._logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", _connection.Endpoint.HostName);

                    return true;
                }

                this._logger.LogCritical("Fatal error: RabbitMQ connections could not be created and opened");

                return false;
            }
        }

        /// <summary>
        /// Called when [connection blocked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ConnectionBlockedEventArgs"/> instance containing the event data.</param>
        private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (this._disposed)
            {
                return;
            }

            this._logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        /// <summary>
        /// Called when [callback exception].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CallbackExceptionEventArgs"/> instance containing the event data.</param>
        private void OnCallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (this._disposed)
            {
                return;
            }

            this._logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        /// <summary>
        /// Called when [connection shutdown].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="reason">The <see cref="ShutdownEventArgs"/> instance containing the event data.</param>
        private void OnConnectionShutdown(object? sender, ShutdownEventArgs reason)
        {
            if (this._disposed)
            {
                return;
            }

            this._logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }

    }
}