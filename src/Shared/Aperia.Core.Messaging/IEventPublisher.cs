using Aperia.Core.Messaging.Models;
using ErrorOr;

namespace Aperia.Core.Messaging;

/// <summary>
/// The IEventPublisher interface
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// Publishes the event asynchronous.
    /// </summary>
    /// <param name="event">The event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<ErrorOr<PublishResult>> PublishAsync(Event @event, CancellationToken cancellationToken = default);
}