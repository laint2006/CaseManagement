namespace Aperia.Core.Domain.Primitives;

/// <summary>
/// The ISoftDeleteEntity interface
/// </summary>
public interface ISoftDeleteEntity
{
    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

}