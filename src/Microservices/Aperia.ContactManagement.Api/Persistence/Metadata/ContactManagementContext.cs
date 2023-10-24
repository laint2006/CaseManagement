using Microsoft.EntityFrameworkCore;

namespace Aperia.ContactManagement.Api.Persistence;

/// <summary>
/// The Contact Management Context
/// </summary>
/// <seealso cref="Aperia.Core.Persistence.BaseDbContext{ContactManagementContext}" />
public partial class ContactManagementContext : BaseDbContext<ContactManagementContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactManagementContext"/> class.
    /// </summary>
    /// <param name="appContext">The application context.</param>
    /// <param name="options">The options.</param>
    public ContactManagementContext(IAppContext appContext, DbContextOptions<ContactManagementContext> options)
        : base(appContext, options)
    {
    }
}