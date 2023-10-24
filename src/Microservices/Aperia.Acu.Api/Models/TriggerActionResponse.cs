using Aperia.Core.Application.Contracts;

namespace Aperia.Acu.Api.Models;

/// <summary>
/// The Trigger Action Response
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Response" />
public class TriggerActionResponse : Response
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public Guid Id { get; set; }

}