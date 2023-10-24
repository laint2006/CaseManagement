
using Aperia.Core.Domain.Primitives;

namespace Aperia.SlaOla.Api.Entities;

/// <summary>
/// The La Object
/// </summary>
/// <seealso cref="Aperia.Core.Domain.Primitives.Entity{Guid}" />
/// <seealso cref="Aperia.Core.Domain.Primitives.IAuditableEntity" />
public class LaObject : Entity<Guid>, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the source.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the object identifier.
    /// </summary>
    public Guid ObjectId { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the la change histories.
    /// </summary>
    public virtual ICollection<LaChangeHistory> LaChangeHistories { get; set; } = new List<LaChangeHistory>();

    /// <summary>
    /// Initializes a new instance of the <see cref="LaObject" /> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="objectId">The object identifier.</param>
    private LaObject(string source, Guid objectId)
        : base(Guid.NewGuid())
    {
        this.Source = source;
        this.ObjectId = objectId;
    }

    /// <summary>
    /// Adds the change history.
    /// </summary>
    /// <param name="changeId">The change identifier.</param>
    /// <param name="attribute">The attribute.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public ErrorOr<LaChangeHistory> AddChangeHistory(Guid changeId, string attribute, string? value)
    {
        var changeHistory = LaChangeHistory.Create(changeId, this.Id, attribute, value);
        this.LaChangeHistories.Add(changeHistory);

        return changeHistory;
    }

    /// <summary>
    /// Creates the la object.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="objectId">The object identifier.</param>
    /// <returns></returns>
    public static ErrorOr<LaObject> Create(string source, Guid objectId)
    {
        var laObject = new LaObject(source, objectId);
        laObject.AddDomainEvent(DomainEvent.Create("LaObject.Created", laObject.Id.ToString(), laObject));

        return laObject;
    }

}