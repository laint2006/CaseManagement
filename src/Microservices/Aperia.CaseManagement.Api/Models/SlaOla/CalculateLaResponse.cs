using Aperia.Core.Application.Contracts;

namespace Aperia.CaseManagement.Api.Models.SlaOla;

/// <summary>
/// The Calculate La Response
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Response" />
public class CalculateLaResponse : Response
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public Guid Id { get; set; }

}