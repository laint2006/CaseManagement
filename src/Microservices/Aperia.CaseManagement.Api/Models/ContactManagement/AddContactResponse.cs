using Aperia.Core.Application.Contracts;

namespace Aperia.CaseManagement.Api.Models.ContactManagement;

/// <summary>
/// The Add Contact Response
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Response" />
public class AddContactResponse : Response
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the object identifier.
    /// </summary>
    public Guid ObjectId { get; set; }

    /// <summary>
    /// Gets or sets the name of the contact.
    /// </summary>
    public string? ContactName { get; set; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }

}