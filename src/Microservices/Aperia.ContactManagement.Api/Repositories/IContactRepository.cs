using Aperia.ContactManagement.Api.Entities;
using Aperia.Core.Application.Repositories;

namespace Aperia.ContactManagement.Api.Repositories;

/// <summary>
/// The IContactRepository interface
/// </summary>
public interface IContactRepository : IRepository<Contact>
{
    /// <summary>
    /// Gets the by contact name asynchronous.
    /// </summary>
    /// <param name="objectId">The object identifier.</param>
    /// <param name="contactName">Name of the contact.</param>
    /// <param name="phoneNumber">The phone number.</param>
    /// <returns></returns>
    Task<Contact?> GetByContactNameAsync(Guid objectId, string contactName, string phoneNumber);
}