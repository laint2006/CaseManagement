using Aperia.Core.Application.Services;
using Aperia.Core.Persistence.Converters;

namespace Aperia.SlaOla.Api.Persistence;

/// <summary>
/// The Unit Of Work
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseUnitOfWork{SlaOlaContext}" />
public class UnitOfWork : BaseUnitOfWork<SlaOlaContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="outboxMessageConverter">The outbox message converter.</param>
    public UnitOfWork(IAppContext appContext, IDateTimeProvider dateTimeProvider, SlaOlaContext dbContext, IOutboxMessageConverter outboxMessageConverter)
        : base(appContext, dateTimeProvider, dbContext, outboxMessageConverter)
    {
    }
}