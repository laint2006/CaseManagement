using Aperia.Core.Application.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Aperia.Core.Persistence.Converters
{
    /// <summary>
    /// The Outbox Message Converter
    /// </summary>
    /// <seealso cref="Aperia.Core.Persistence.Converters.IOutboxMessageConverter" />
    public class OutboxMessageConverter : IOutboxMessageConverter
    {
        /// <summary>
        /// The json serializer options
        /// </summary>
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            WriteIndented = false,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { IgnoreDomainEvents }
            }
        };

        /// <summary>
        /// The date time provider
        /// </summary>
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutboxMessageConverter" /> class.
        /// </summary>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public OutboxMessageConverter(IDateTimeProvider dateTimeProvider)
        {
            this._dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Converts the given event to outbox message.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="event">The event.</param>
        /// <returns></returns>
        public OutboxMessage? Convert(IHasDomainEvents entity, IDomainEvent @event)
        {
            var payload = @event.Payload is null ? null : System.Text.Json.JsonSerializer.Serialize(@event.Payload, JsonSerializerOptions);

            return OutboxMessage.Create(@event.EntityId, @event.EventType, payload, _dateTimeProvider.Now);
        }

        /// <summary>
        /// Ignores the domain events.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <returns></returns>
        private static void IgnoreDomainEvents(JsonTypeInfo typeInfo)
        {
            foreach (var propertyInfo in typeInfo.Properties)
            {
                if ("DomainEvents".Equals(propertyInfo.PropertyType.Name, StringComparison.OrdinalIgnoreCase))
                {
                    propertyInfo.ShouldSerialize = static (_, _) => false;
                }
            }
        }

    }
}