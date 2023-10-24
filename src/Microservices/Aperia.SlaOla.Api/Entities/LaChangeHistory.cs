using Aperia.Core.Domain.Primitives;

namespace Aperia.SlaOla.Api.Entities;

/// <summary>
/// The La Change History
/// </summary>
/// <seealso cref="Aperia.Core.Domain.Primitives.Entity{Int64}" />
/// <seealso cref="Aperia.Core.Domain.Primitives.IAuditableEntity" />
public class LaChangeHistory : Entity<long>, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the change identifier.
    /// </summary>
    public Guid ChangeId { get; set; }

    /// <summary>
    /// Gets or sets the la object identifier.
    /// </summary>
    public Guid LaObjectId { get; set; }

    /// <summary>
    /// Gets or sets the attribute.
    /// </summary>
    public string Attribute { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the la object.
    /// </summary>
    public virtual LaObject LaObject { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="LaChangeHistory" /> class.
    /// </summary>
    /// <param name="changeId">The change identifier.</param>
    /// <param name="laObjectId">The la object identifier.</param>
    /// <param name="attribute">The attribute.</param>
    /// <param name="value">The value.</param>
    private LaChangeHistory(Guid changeId, Guid laObjectId, string attribute, string? value)
        : base(default)
    {
        this.ChangeId = changeId;
        this.LaObjectId = laObjectId;
        this.Attribute = attribute;
        this.Value = value;
    }

    /// <summary>
    /// Creates the change history.
    /// </summary>
    /// <param name="changeId">The change identifier.</param>
    /// <param name="laObjectId">The la object identifier.</param>
    /// <param name="attribute">The attribute.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static LaChangeHistory Create(Guid changeId, Guid laObjectId, string attribute, string? value)
    {
        var contact = new LaChangeHistory(changeId, laObjectId, attribute, value);

        return contact;
    }

}