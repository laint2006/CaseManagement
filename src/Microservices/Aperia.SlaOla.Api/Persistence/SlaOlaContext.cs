using Aperia.SlaOla.Api.Entities;
using Aperia.SlaOla.Api.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Aperia.SlaOla.Api.Persistence;

public partial class SlaOlaContext
{
    public virtual DbSet<LaChangeHistory> LaChangeHistories { get; set; }

    public virtual DbSet<LaObject> LaObjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LaChangeHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new LaObjectConfiguration());
    }
}