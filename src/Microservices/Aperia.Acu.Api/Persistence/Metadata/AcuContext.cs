using Microsoft.EntityFrameworkCore;

namespace Aperia.Acu.Api.Persistence;

/// <summary>
/// The Contact Management Context
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseDbContext{AcuContext}" />
public partial class AcuContext : BaseDbContext<AcuContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AcuContext"/> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="options">The options.</param>
    public AcuContext(IAppContext appContext, DbContextOptions<AcuContext> options)
        : base(appContext, options)
    {
    }
}