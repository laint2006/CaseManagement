namespace Aperia.Core.Persistence;

/// <summary>
/// The Base Repository
/// </summary>
/// <typeparam name="TContext">The type of the context.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="Aperia.Core.Persistence.BaseRepository{TContext, TEntity, Guid}" />
/// <seealso cref="Aperia.Core.Application.Repositories.IRepository{TEntity}" />
public abstract class BaseRepository<TContext, TEntity> : BaseRepository<TContext, TEntity, Guid>, IRepository<TEntity>
    where TContext : DbContext
    where TEntity : Entity<Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{TContext, TEntity}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    protected BaseRepository(TContext dbContext)
        : base(dbContext)
    {
    }
}

/// <summary>
/// The Base Repository
/// </summary>
/// <typeparam name="TContext">The type of the context.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the identifier.</typeparam>
/// <seealso cref="Aperia.Core.Persistence.BaseRepository{TContext, TEntity, Guid}" />
/// <seealso cref="Aperia.Core.Application.Repositories.IRepository{TEntity}" />
public abstract class BaseRepository<TContext, TEntity, TId> : IRepository<TEntity, TId>
    where TContext : DbContext
    where TEntity : Entity<TId>
    where TId : notnull
{
    /// <summary>
    /// The database context
    /// </summary>
    protected TContext DbContext { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{TEntity, TId}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    protected BaseRepository(TContext dbContext)
    {
        this.DbContext = dbContext;
    }

    /// <summary>
    /// Gets all entities asynchronous.
    /// </summary>
    /// <returns></returns>
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await this.DbContext.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Gets the entity by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await this.DbContext.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Gets the queryable.
    /// </summary>
    /// <returns></returns>
    public IQueryable<TEntity> GetQueryable()
    {
        return this.DbContext.Set<TEntity>();
    }

    /// <summary>
    /// Adds the given entity to the entities set.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Add(TEntity entity)
    {
        this.DbContext.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// Adds the given entity to the entities set asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public async Task AddAsync(TEntity entity)
    {
        await this.DbContext.Set<TEntity>().AddAsync(entity);
    }

    /// <summary>
    /// Updates the given entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Update(TEntity entity)
    {
        this.DbContext.Set<TEntity>().Update(entity);
    }

    /// <summary>
    /// Deletes the given entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Delete(TEntity entity)
    {
        this.DbContext.Set<TEntity>().Remove(entity);
    }

}