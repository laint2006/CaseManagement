using Aperia.Core.Application.Contracts;
using Aperia.Ownership.Api.Entities;

namespace Aperia.Ownership.Api.Models;

/// <summary>
/// The Get Owner Response
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Response" />
public class GetOwnerResponse : Response
{
    /// <summary>
    /// Gets or sets the name of the contact.
    /// </summary>
    public OwnerType? OwnerType { get; set; }

    /// <summary>
    /// Gets or sets the object identifier.
    /// </summary>
    public Guid? OwnerId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }

}