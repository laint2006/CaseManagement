namespace Aperia.Core.Messaging.RabbitMq
{
    /// <summary>
    /// The IRabbitMqConnectionManager interface
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IRabbitMqConnectionManager : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Tries the connect.
        /// </summary>
        /// <returns></returns>
        bool TryConnect();

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
    }
}