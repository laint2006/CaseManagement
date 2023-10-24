using Aperia.Core.Application.Contracts;

namespace Aperia.CaseManagement.Api.Models.SlaOla;

/// <summary>
/// The Calculate La Request
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Request" />
public class CalculateLaRequest : Request
{
    /// <summary>
    /// Gets or sets the source.
    /// </summary>
    public string Source { get; set; } = null!;

    /// <summary>
    /// Gets or sets the object identifier.
    /// </summary>
    public Guid ObjectId { get; set; }

    /// <summary>
    /// Gets or sets the change histories.
    /// </summary>
    public List<ChangeHistory>? ChangeHistories { get; set; }
}