namespace Aperia.Core.Persistence.Converters
{
    /// <summary>
    /// The IOutboxMessageConverter interface
    /// </summary>
    public interface IOutboxMessageConverter
    {
        /// <summary>
        /// Converts the given event to outbox message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="event">The event.</param>
        /// <returns></returns>
        OutboxMessage? Convert(IHasDomainEvents entity, IDomainEvent @event);

    }
}