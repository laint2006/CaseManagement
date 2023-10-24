using System.Text.Json;
using Aperia.Core.Messaging.Models;
using ErrorOr;

namespace Aperia.Core.Messaging.RabbitMq
{
    /// <summary>
    /// The Rabbit Mq Publisher
    /// </summary>
    /// <seealso cref="Aperia.Core.Messaging.IEventPublisher" />
    public class RabbitMqPublisher : IEventPublisher
    {
        public async Task<ErrorOr<PublishResult>> PublishAsync(Event message, CancellationToken cancellationToken = default)
        {
            Console.WriteLine(nameof(PublishAsync) + ": " + JsonSerializer.Serialize(message));

            await Task.CompletedTask;
            return new PublishResult();
        }
    }
}