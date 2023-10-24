using Aperia.Acu.Api.Entities;
using Aperia.Acu.Api.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Aperia.Acu.Api.Persistence;

public partial class AcuContext
{
    public virtual DbSet<TriggerRequest> Contacts { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TriggerRequestConfiguration());
    }
}