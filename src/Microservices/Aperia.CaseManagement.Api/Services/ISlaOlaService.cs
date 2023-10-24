using Aperia.CaseManagement.Api.Models.SlaOla;

namespace Aperia.CaseManagement.Api.Services;

/// <summary>
/// The ISlaOlaService interface
/// </summary>
public interface  ISlaOlaService
{
    /// <summary>
    /// Calculates the la asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<CalculateLaResponse?> CalculateLaAsync(CalculateLaRequest request);
}