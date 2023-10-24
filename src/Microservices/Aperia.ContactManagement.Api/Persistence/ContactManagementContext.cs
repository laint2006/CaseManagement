using Aperia.ContactManagement.Api.Entities;
using Aperia.ContactManagement.Api.Persistence.Configurations;
using Aperia.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aperia.ContactManagement.Api.Persistence;

public partial class ContactManagementContext
{
    public virtual DbSet<Contact> Contacts { get; set; } = null!;

    public virtual DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }
}