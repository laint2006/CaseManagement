using Aperia.Ownership.Api.Entities;
using Aperia.Ownership.Api.Persistence;
using Aperia.Ownership.Api.Queries.GetOwner;
using Microsoft.EntityFrameworkCore;

namespace Aperia.Ownership.Api.Repositories
{
    /// <summary>
    /// The Contact Repository
    /// </summary>
    /// <seealso cref="Aperia.Ownership.Api.Repositories.Repository{Owner}" />
    /// <seealso cref="Aperia.Ownership.Api.Repositories.IOwnerRepository" />
    public class OwnerRepository : Repository<Owner, long>, IOwnerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public OwnerRepository(OwnershipContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Gets the owner asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public async Task<Owner?> GetOwnerAsync(GetOwnerQuery query)
        {
            return await this.DbContext.Owners.OrderBy(x=> Guid.NewGuid()).FirstOrDefaultAsync();

        }
    }
}