using Aperia.Acu.Api.Entities;
using Aperia.Acu.Api.Persistence;

namespace Aperia.Acu.Api.Repositories
{
    /// <summary>
    /// The Trigger Repository
    /// </summary>
    /// <seealso cref="Aperia.Acu.Api.Repositories.Repository{TriggerRequest}" />
    /// <seealso cref="Aperia.Acu.Api.Repositories.ITriggerRepository" />
    public class TriggerRepository : Repository<TriggerRequest>, ITriggerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public TriggerRepository(AcuContext dbContext)
            : base(dbContext)
        {
        }
    }
}