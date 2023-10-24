#nullable enable
using Aperia.Core.Domain.Primitives;

namespace Aperia.ContactManagement.Api.Entities;

public class Contact : Entity<Guid>, IAuditableEntity
{
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

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Contact" /> class.
    /// </summary>
    /// <param name="objectId">The object identifier.</param>
    /// <param name="contactName">Name of the contact.</param>
    /// <param name="phoneNumber">The phone number.</param>
    private Contact(Guid objectId, string contactName, string phoneNumber)
        : base(Guid.NewGuid())
    {
        this.ObjectId = objectId;
        this.ContactName = contactName;
        this.PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Creates the specified customer name.
    /// </summary>
    /// <param name="objectId">The object identifier.</param>
    /// <param name="contactName">Name of the contact.</param>
    /// <param name="phoneNumber">The phone number.</param>
    /// <returns></returns>
    public static Contact Create(Guid objectId, string contactName, string phoneNumber)
    {
        var contact = new Contact(objectId, contactName, phoneNumber);
        contact.AddDomainEvent(DomainEvent.Create("Contact.Created", contact.Id.ToString(), contact));

        return contact;
    }
}