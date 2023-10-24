namespace Aperia.Core.Persistence;

/// <summary>
/// The Base Database Context
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
public abstract class BaseDbContext<TContext> : DbContext
    where TContext : DbContext
{
    /// <summary>
    /// Gets or sets the application context.
    /// </summary>
    protected IAppContext AppContext { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDbContext{TContext}" /> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    protected BaseDbContext(IAppContext appContext)
    {
        this.AppContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDbContext{TContext}" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="appContext">The application context.</param>
    protected BaseDbContext(IAppContext appContext, DbContextOptions<TContext> options)
        : base(options)
    {
        this.AppContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
    }
}