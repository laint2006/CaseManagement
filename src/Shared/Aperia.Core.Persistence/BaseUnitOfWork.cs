using Aperia.Core.Application.Services;

namespace Aperia.Core.Persistence;

/// <summary>
/// The Base Unit Of Work
/// </summary>
/// <seealso cref="IUnitOfWork" />
public abstract class BaseUnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
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
    /// The json serializer
    /// </summary>
    protected IJsonSerializer JsonSerializer { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseUnitOfWork{TContext}" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="jsonSerializer">The json serializer.</param>
    protected BaseUnitOfWork(IAppContext appContext, IDateTimeProvider dateTimeProvider, TContext dbContext, IJsonSerializer jsonSerializer)
    {
        this.AppContext = appContext;
        this.DateTimeProvider = dateTimeProvider;
        this.DbContext = dbContext;
        this.JsonSerializer = jsonSerializer;
    }

    /// <summary>
    /// Saves the changes.
    /// </summary>
    /// <returns></returns>
    public abstract int SaveChanges();

    /// <summary>
    /// Saves the changes asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public abstract Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

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

        var currentTime = this.DateTimeProvider.Now;

        foreach (var entity in entities)
        {
            if (entity.DomainEvents.Count == 0)
            {
                continue;
            }

            foreach (var domainEvent in entity.DomainEvents)
            {
                var payload = domainEvent.Payload is null ? null : this.JsonSerializer.Serialize(domainEvent.Payload);
                var message = OutboxMessage.Create(domainEvent.EntityId, domainEvent.EventType, payload, currentTime);

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

}