using Aperia.CaseManagement.Api.Models.Acu;

namespace Aperia.CaseManagement.Api.Services;

/// <summary>
/// The IAcuService interface
/// </summary>
public interface IAcuService
{
    /// <summary>
    /// Triggers the action asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<TriggerActionResponse?> TriggerActionAsync(TriggerActionRequest request);
}