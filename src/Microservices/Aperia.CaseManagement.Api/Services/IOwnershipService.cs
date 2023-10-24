using Aperia.CaseManagement.Api.Models.Ownership;

namespace Aperia.CaseManagement.Api.Services;

/// <summary>
/// The IOwnershipService interface
/// </summary>
public interface IOwnershipService
{
    /// <summary>
    /// Gets the owner asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<GetOwnerResponse?> GetOwnerAsync(GetOwnerRequest request);
}