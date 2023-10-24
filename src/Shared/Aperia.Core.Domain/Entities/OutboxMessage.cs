using Aperia.Core.Domain.Primitives;

namespace Aperia.Core.Domain.Entities;

/// <summary>
/// The Outbox Message
/// </summary>
/// <seealso cref="Aperia.Core.Domain.Primitives.Entity{Guid}" />
/// <seealso cref="Aperia.Core.Domain.Primitives.IAuditableEntity" />
public class OutboxMessage : Entity<Guid>, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    public string EventType { get; set; }

    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public string? EntityId { get; set; }

    /// <summary>
    /// Gets or sets the payload.
    /// </summary>
    public string? Payload { get; set; }

    /// <summary>
    /// Gets or sets the is dispatched.
    /// </summary>
    public bool IsDispatched { get; set; }

    /// <summary>
    /// Gets or sets the processed date.
    /// </summary>
    public DateTime? ProcessedDate { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutboxMessage" /> class.
    /// </summary>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="eventType">Type of the event.</param>
    /// <param name="payload">The payload.</param>
    /// <param name="createdDate">The created date.</param>
    private OutboxMessage(string? entityId, string eventType, string? payload, DateTime createdDate)
        : base(Guid.NewGuid())
    {
        this.EntityId = entityId;
        this.EventType = eventType;
        this.Payload = payload;
        this.CreatedDate = createdDate;
    }

    /// <summary>
    /// Creates the event.
    /// </summary>
    /// <param name="entityId">The entity identifier.</param>
    /// <param name="eventType">Type of the event.</param>
    /// <param name="payload">The payload.</param>
    /// <param name="createdDate">The created date.</param>
    /// <returns></returns>
    public static OutboxMessage Create(string? entityId, string eventType, string? payload, DateTime createdDate)
    {
        return new OutboxMessage(entityId, eventType, payload, createdDate);
    }

}