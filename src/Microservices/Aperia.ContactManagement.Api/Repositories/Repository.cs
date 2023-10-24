using Aperia.ContactManagement.Api.Persistence;
using Aperia.Core.Domain.Primitives;

namespace Aperia.ContactManagement.Api.Repositories
{
    /// <summary>
    /// The Repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Aperia.Core.Persistence.BaseRepository{ContactManagementContext, TEntity}" />
    public abstract class Repository<TEntity> : BaseRepository<ContactManagementContext, TEntity>
        where TEntity : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        protected Repository(ContactManagementContext dbContext)
            : base(dbContext)
        {
        }
    }

    /// <summary>
    /// The Repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <seealso cref="Aperia.Core.Persistence.BaseRepository{ContactManagementContext, TEntity}" />
    public abstract class Repository<TEntity, TId> : BaseRepository<ContactManagementContext, TEntity, TId>
        where TEntity : Entity<TId>
        where TId : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        protected Repository(ContactManagementContext dbContext)
            : base(dbContext)
        {
        }
    }
}