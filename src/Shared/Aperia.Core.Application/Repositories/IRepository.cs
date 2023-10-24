namespace Aperia.Core.Application.Repositories;

/// <summary>
/// The IRepository interface
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity> : IRepository<TEntity, Guid>
    where TEntity : Entity<Guid>
{
}

/// <summary>
/// The IRepository interface
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public interface IRepository<TEntity, in TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets all entities asynchronous.
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Gets the entity by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(TId id);

    /// <summary>
    /// Gets the queryable.
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetQueryable();

    /// <summary>
    /// Adds the given entity to the entities set.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Add(TEntity entity);

    /// <summary>
    /// Adds the given entity to the entities set asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Updates the given entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Deletes the given entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Delete(TEntity entity);

}