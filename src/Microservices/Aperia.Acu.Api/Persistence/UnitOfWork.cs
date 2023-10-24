using Aperia.Core.Application.Services;
using Aperia.Core.Persistence.Converters;

namespace Aperia.Acu.Api.Persistence;

/// <summary>
/// The Unit Of Work
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseUnitOfWork{AcuContext}" />
public class UnitOfWork : BaseUnitOfWork<AcuContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="outboxMessageConverter">The outbox message converter.</param>
    public UnitOfWork(IAppContext appContext, IDateTimeProvider dateTimeProvider, AcuContext dbContext, IOutboxMessageConverter outboxMessageConverter)
        : base(appContext, dateTimeProvider, dbContext, outboxMessageConverter)
    {
    }

}