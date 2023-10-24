using Aperia.Core.Application.Contracts;

namespace Aperia.CaseManagement.Api.Models.Ownership;

/// <summary>
/// The Get Owner Request
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Request" />
public class GetOwnerRequest : Request
{
    /// <summary>
    /// Gets or sets the source.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the type of the object.
    /// </summary>
    public string? ObjectType { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the secondary status.
    /// </summary>
    public string? SecondaryStatus { get; set; }

}