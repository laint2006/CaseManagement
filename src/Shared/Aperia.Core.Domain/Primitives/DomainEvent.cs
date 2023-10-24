namespace Aperia.Core.Domain.Primitives;

/// <summary>
/// The Domain Event
/// </summary>
/// <seealso cref="IDomainEvent" />
public class DomainEvent : IDomainEvent
{
    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    public string EventType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public string? EntityId { get; set; }

    /// <summary>
    /// Gets or sets the payload.
    /// </summary>
    public object? Payload { get; set; }

    /// <summary>
    /// Creates the domain event.
    /// </summary>
    /// <param name="eventType">TransactionType of the event.</param>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="payload">The payload.</param>
    /// <returns></returns>
    public static DomainEvent Create(string eventType, string? entityId, object? payload)
    {
        return new DomainEvent
        {
            EventType = eventType,
            EntityId = entityId,
            Payload = payload
        };
    }
}