namespace Aperia.Core.Domain.Primitives;

/// <summary>
/// The IHasDomainEvents interface
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gets the domain events.
    /// </summary>
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears the domain events.
    /// </summary>
    void ClearDomainEvents();

}