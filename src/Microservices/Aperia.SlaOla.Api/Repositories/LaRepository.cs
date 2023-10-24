using Aperia.SlaOla.Api.Entities;
using Aperia.SlaOla.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Aperia.SlaOla.Api.Repositories
{
    /// <summary>
    /// The Contact Repository
    /// </summary>
    /// <seealso cref="Aperia.SlaOla.Api.Repositories.Repository{Contact}" />
    /// <seealso cref="Aperia.SlaOla.Api.Repositories.ILaRepository" />
    public class LaRepository : Repository<LaObject>, ILaRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LaRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public LaRepository(SlaOlaContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Gets the by object identifier asynchronous.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <returns></returns>
        public async Task<LaObject?> GetByObjectIdAsync(string source, Guid objectId)
        {
            return await this.DbContext.LaObjects.FirstOrDefaultAsync(x => x.Source == source && x.ObjectId == objectId);
        }
    }
}