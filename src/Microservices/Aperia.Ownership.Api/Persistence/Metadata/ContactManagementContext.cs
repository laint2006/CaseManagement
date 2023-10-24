using Microsoft.EntityFrameworkCore;

namespace Aperia.Ownership.Api.Persistence;

/// <summary>
/// The Contact Management Context
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseDbContext{OwnershipContext}" />
public partial class OwnershipContext : BaseDbContext<OwnershipContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OwnershipContext"/> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="options">The options.</param>
    public OwnershipContext(IAppContext appContext, DbContextOptions<OwnershipContext> options)
        : base(appContext, options)
    {
    }
}