using Aperia.Core.Application.Contracts;

namespace Aperia.CaseManagement.Api.Models.ContactManagement;

/// <summary>
/// The Add Contact Request
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Request" />
public class AddContactRequest : Request
{
    /// <summary>
    /// Gets or sets the object identifier.
    /// </summary>
    public Guid ObjectId { get; set; }

    /// <summary>
    /// Gets or sets the name of the contact.
    /// </summary>
    public string ContactName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

}
