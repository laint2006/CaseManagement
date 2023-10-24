using Microsoft.EntityFrameworkCore;

namespace Aperia.CaseManagement.Api.Persistence;

/// <summary>
/// The Contact Management Context
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseDbContext{CaseManagementContext}" />
public partial class CaseManagementContext : BaseDbContext<CaseManagementContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CaseManagementContext"/> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="options">The options.</param>
    public CaseManagementContext(IAppContext appContext, DbContextOptions<CaseManagementContext> options)
        : base(appContext, options)
    {
    }
}