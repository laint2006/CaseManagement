using Aperia.ContactManagement.Api.Entities;
using Aperia.ContactManagement.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Aperia.ContactManagement.Api.Repositories
{
    /// <summary>
    /// The Contact Repository
    /// </summary>
    /// <seealso cref="Aperia.ContactManagement.Api.Repositories.Repository{Contact}" />
    /// <seealso cref="Aperia.ContactManagement.Api.Repositories.IContactRepository" />
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ContactRepository(ContactManagementContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Gets the by contact name asynchronous.
        /// </summary>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="contactName">Name of the contact.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        public async Task<Contact?> GetByContactNameAsync(Guid objectId, string contactName, string phoneNumber)
        {
            return await this.DbContext.Contacts
                                        .FirstOrDefaultAsync(x => x.ObjectId == objectId && x.ContactName == contactName && x.PhoneNumber == phoneNumber);
        }
    }
}