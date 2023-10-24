using Aperia.Core.Application.Services;
using Aperia.Core.Persistence.Converters;

namespace Aperia.ContactManagement.Api.Persistence;

/// <summary>
/// The Unit Of Work
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseUnitOfWork{ContactManagementContext}" />
public class UnitOfWork : BaseUnitOfWork<ContactManagementContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="outboxMessageConverter">The outbox message converter.</param>
    public UnitOfWork(IAppContext appContext, IDateTimeProvider dateTimeProvider, ContactManagementContext dbContext, IOutboxMessageConverter outboxMessageConverter)
        : base(appContext, dateTimeProvider, dbContext, outboxMessageConverter)
    {
    }

    /// <summary>
    /// Saves the changes.
    /// </summary>
    /// <returns></returns>
    public override int SaveChanges()
    {
        this.UpdateAuditableEntities();

        var outboxMessages = this.GetOutboxMessages();
        this.DbContext.OutboxMessages.AddRange(outboxMessages);

        return this.DbContext.SaveChanges();
    }

    /// <summary>
    /// Saves the changes asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.UpdateAuditableEntities();

        var outboxMessages = this.GetOutboxMessages();
        await this.DbContext.OutboxMessages.AddRangeAsync(outboxMessages, cancellationToken);

        return await this.DbContext.SaveChangesAsync(cancellationToken);
    }
}