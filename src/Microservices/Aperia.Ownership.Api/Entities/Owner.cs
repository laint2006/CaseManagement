using Aperia.Core.Domain.Primitives;

namespace Aperia.Ownership.Api.Entities;

/// <summary>
/// The Owner
/// </summary>
/// <seealso cref="Aperia.Core.Domain.Primitives.Entity{Int64}" />
/// <seealso cref="Aperia.Core.Domain.Primitives.IAuditableEntity" />
public class Owner : Entity<long>, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the type of the owner.
    /// </summary>
    public OwnerType OwnerType { get; set; }

    /// <summary>
    /// Gets or sets the owner identifier.
    /// </summary>
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Owner" /> class.
    /// </summary>
    /// <param name="ownerType">Type of the owner.</param>
    /// <param name="ownerId">The owner identifier.</param>
    /// <param name="name">The name.</param>
    private Owner(OwnerType ownerType, Guid ownerId, string name)
        : base(default)
    {
        this.OwnerType = ownerType;
        this.OwnerId = ownerId;
        this.Name = name;
    }

    /// <summary>
    /// Creates the owner.
    /// </summary>
    /// <param name="ownerType">Type of the owner.</param>
    /// <param name="ownerId">The owner identifier.</param>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public static Owner Create(OwnerType ownerType, Guid ownerId, string name)
    {
        var owner = new Owner(ownerType, ownerId, name);
        owner.AddDomainEvent(DomainEvent.Create("Owner.Created", owner.Id.ToString(), owner));

        return owner;
    }
}