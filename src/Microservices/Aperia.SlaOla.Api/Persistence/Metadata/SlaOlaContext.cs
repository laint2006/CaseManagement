using Microsoft.EntityFrameworkCore;

namespace Aperia.SlaOla.Api.Persistence;

/// <summary>
/// The SlaOla Context
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseDbContext{SlaOlaContext}" />
public partial class SlaOlaContext : BaseDbContext<SlaOlaContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SlaOlaContext"/> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="options">The options.</param>
    public SlaOlaContext(IAppContext appContext, DbContextOptions<SlaOlaContext> options)
        : base(appContext, options)
    {
    }
}