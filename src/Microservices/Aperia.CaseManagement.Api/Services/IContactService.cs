using Aperia.CaseManagement.Api.Models.ContactManagement;

namespace Aperia.CaseManagement.Api.Services;

/// <summary>
/// The IContactService interface
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Adds the contact asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    Task<AddContactResponse?> AddContactAsync(AddContactRequest request);
}