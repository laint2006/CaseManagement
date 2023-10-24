namespace Aperia.Core.Domain.Primitives;

/// <summary>
/// The IAuditableEntity interface
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    DateTime? UpdatedDate { get; set; }

}