using Aperia.CaseManagement.Api.Entities;
using Aperia.Core.Application.Contracts;

namespace Aperia.CaseManagement.Api.Models;

/// <summary>
/// The Add Inquiry Request
/// </summary>
/// <seealso cref="Aperia.Core.Application.Contracts.Request" />
public class CreateInquiryRequest : Request
{
    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public string? EntityId { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public InquiryStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the secondary status.
    /// </summary>
    public string? SecondaryStatus { get; set; }

    /// <summary>
    /// Gets or sets the name of the contact.
    /// </summary>
    public string ContactName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

}