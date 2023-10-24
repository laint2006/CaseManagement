namespace Aperia.Core.Domain.Primitives;

/// <summary>
/// The Entity
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
/// <seealso cref="System.IEquatable{Entity}" />
public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
    where TId : notnull
{
    #region Variables

    /// <summary>
    /// The domain events
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = new();

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public TId Id { get; protected set; }

    /// <summary>
    /// Gets the domain events.
    /// </summary>
    public IReadOnlyList<IDomainEvent> DomainEvents => this._domainEvents.AsReadOnly();

    #endregion

    #region Contructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    protected Entity(TId id)
    {
        this.Id = id;
    }

    #endregion

    #region Operators

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    public static bool operator ==(Entity<TId> left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Adds the domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event.</param>
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        this._domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Clears the domain events.
    /// </summary>
    public void ClearDomainEvents()
    {
        this._domainEvents.Clear();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && this.Id.Equals(entity.Id);
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns></returns>
    public bool Equals(Entity<TId>? other)
    {
        return other is not null && this.Id.Equals(other.Id);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    #endregion

}