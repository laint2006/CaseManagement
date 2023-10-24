using Aperia.Core.Application.Contracts;

namespace Aperia.CaseManagement.Api.Models.Acu;

/// <summary>
/// The Trigger Action Request
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Request" />
public class TriggerActionRequest : Request
{
    /// <summary>
    /// Gets or sets the trigger point code.
    /// </summary>
    public string TriggerPointCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    public string EventType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the request data.
    /// </summary>
    public string? RequestData { get; set; }

}