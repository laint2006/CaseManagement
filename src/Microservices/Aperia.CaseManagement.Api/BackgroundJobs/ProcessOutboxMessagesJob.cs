using Aperia.CaseManagement.Api.Persistence;
using Aperia.Core.Application.Services;
using Aperia.Core.Messaging;
using Aperia.Core.Messaging.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Aperia.CaseManagement.Api.BackgroundJobs;

/// <summary>
/// The Process Outbox Messages Job
/// </summary>
/// <seealso cref="Quartz.IJob" />
[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    /// <summary>
    /// The application context
    /// </summary>
    private readonly IAppContext _appContext;

    /// <summary>
    /// The database context
    /// </summary>
    private readonly CaseManagementContext _dbContext;

    /// <summary>
    /// The publisher
    /// </summary>
    private readonly IEventPublisher _publisher;

    /// <summary>
    /// The date time provider
    /// </summary>
    private readonly IDateTimeProvider _dateTimeProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessOutboxMessagesJob" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="publisher">The publisher.</param>
    public ProcessOutboxMessagesJob(IAppContext appContext, IDateTimeProvider dateTimeProvider, CaseManagementContext dbContext, IEventPublisher publisher)
    {
        this._appContext = appContext;
        this._dbContext = dbContext;
        this._publisher = publisher;
        this._dateTimeProvider = dateTimeProvider;
    }

    /// <summary>
    /// Called by the <see cref="T:Quartz.IScheduler" /> when a <see cref="T:Quartz.ITrigger" />
    /// fires that is associated with the <see cref="T:Quartz.IJob" />.
    /// </summary>
    /// <param name="context">The execution context.</param>
    /// <remarks>
    /// The implementation may wish to set a  result object on the
    /// JobExecutionContext before this method exits.  The result itself
    /// is meaningless to Quartz, but may be informative to
    /// <see cref="T:Quartz.IJobListener" />s or
    /// <see cref="T:Quartz.ITriggerListener" />s that are watching the job's
    /// execution.
    /// </remarks>
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await this._dbContext.OutboxMessages
            .Where(m => !m.IsDispatched)
            .OrderBy(m => m.CreatedDate)
            .Take(20)
            .ToListAsync(context.CancellationToken);
        var currentDate = this._dateTimeProvider.Now;

        foreach (var outboxMessage in messages)
        {
            var @event = Event.Create(this._appContext.Name, outboxMessage);

            await this._publisher.PublishAsync(@event, context.CancellationToken);

            outboxMessage.IsDispatched = true;
            outboxMessage.ProcessedDate = currentDate;

            await _dbContext.SaveChangesAsync();
        }
    }
}