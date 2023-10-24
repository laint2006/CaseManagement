using Aperia.Ownership.Api.Entities;
using Aperia.Ownership.Api.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Aperia.Ownership.Api.Persistence;

public partial class OwnershipContext
{
    public virtual DbSet<Owner> Owners { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OwnerConfiguration());
    }
}