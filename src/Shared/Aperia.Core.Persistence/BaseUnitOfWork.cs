using Aperia.Core.Application.Services;
using Aperia.Core.Persistence.Converters;

namespace Aperia.Core.Persistence;

/// <summary>
/// The Base Unit Of Work
/// </summary>
/// <seealso cref="IUnitOfWork" />
public abstract class BaseUnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    #region Properties

    /// <summary>
    /// The application context
    /// </summary>
    protected IAppContext AppContext { get; }

    /// <summary>
    /// Gets the date time provider.
    /// </summary>
    protected IDateTimeProvider DateTimeProvider { get; }

    /// <summary>
    /// The database context
    /// </summary>
    protected TContext DbContext { get; }

    /// <summary>
    /// Gets the outbox message converter.
    /// </summary>
    protected IOutboxMessageConverter OutboxMessageConverter { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseUnitOfWork{TContext}" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="outboxMessageConverter">The outbox message converter.</param>
    protected BaseUnitOfWork(IAppContext appContext, IDateTimeProvider dateTimeProvider, TContext dbContext, IOutboxMessageConverter outboxMessageConverter)
    {
        this.AppContext = appContext;
        this.DateTimeProvider = dateTimeProvider;
        this.DbContext = dbContext;
        this.OutboxMessageConverter = outboxMessageConverter;
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Saves the changes.
    /// </summary>
    /// <returns></returns>
    public virtual int SaveChanges()
    {
        this.UpdateAuditableEntities();

        return this.DbContext.SaveChanges();
    }

    /// <summary>
    /// Saves the changes asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.UpdateAuditableEntities();

        return await this.DbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Gets the outbox messages.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<OutboxMessage> GetOutboxMessages()
    {
        var entities = this.DbContext.ChangeTracker.Entries<IHasDomainEvents>()
            .Where(entry => entry.Entity.DomainEvents.Count > 0)
            .Select(entry => entry.Entity)
            .ToList();

        foreach (var entity in entities)
        {
            if (entity.DomainEvents.Count == 0)
            {
                continue;
            }

            foreach (var domainEvent in entity.DomainEvents)
            {
                var message = this.OutboxMessageConverter.Convert(entity, domainEvent);
                if (message is null)
                {
                    continue;
                }

                yield return message;
            }
        }
    }

    /// <summary>
    /// Updates the auditable entities.
    /// </summary>
    /// <returns></returns>
    protected virtual void UpdateAuditableEntities()
    {
        var currentTime = this.DateTimeProvider.Now;

        foreach (var entityEntry in this.DbContext.ChangeTracker.Entries())
        {
            if (entityEntry is { State: EntityState.Deleted, Entity: ISoftDeleteEntity softDeleteEntity })
            {
                softDeleteEntity.IsDeleted = true;
                softDeleteEntity.UpdatedDate = currentTime;

                continue;
            }

            if (entityEntry.Entity is IAuditableEntity auditableEntity)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        auditableEntity.CreatedDate = currentTime;
                        break;

                    case EntityState.Modified:
                        auditableEntity.UpdatedDate = currentTime;
                        break;
                }
            }
        }
    }

    #endregion

}