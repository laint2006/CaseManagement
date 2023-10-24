using Aperia.Core.Domain.Entities;
using System.Text.Json;

namespace Aperia.Core.Messaging.Models
{
    /// <summary>
    /// The Message
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

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
        /// Creates the specified outbox message.
        /// </summary>
        /// <param name="outboxMessage">The outbox message.</param>
        /// <returns></returns>
        public static Event Create(OutboxMessage outboxMessage)
        {
            return new Event
            {
                Id = outboxMessage.Id,
                EntityId = outboxMessage.EntityId,
                EventType = outboxMessage.EventType,
                Payload = string.IsNullOrWhiteSpace(outboxMessage.Payload) ? null : JsonDocument.Parse(outboxMessage.Payload)
            };
        }

    }
}