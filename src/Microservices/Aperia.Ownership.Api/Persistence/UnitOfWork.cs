using Aperia.Core.Application.Services;

namespace Aperia.Ownership.Api.Persistence;

/// <summary>
/// The Unit Of Work
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseUnitOfWork{OwnershipContext}" />
public class UnitOfWork : BaseUnitOfWork<OwnershipContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="jsonSerializer">The json serializer.</param>
    public UnitOfWork(IAppContext appContext, IDateTimeProvider dateTimeProvider, OwnershipContext dbContext, IJsonSerializer jsonSerializer)
        : base(appContext, dateTimeProvider, dbContext, jsonSerializer)
    {
    }

    /// <summary>
    /// Saves the changes.
    /// </summary>
    /// <returns></returns>
    public override int SaveChanges()
    {
        this.UpdateAuditableEntities();

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

        return await this.DbContext.SaveChangesAsync(cancellationToken);
    }
}