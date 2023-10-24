using Aperia.CaseManagement.Api.Entities;
using Aperia.CaseManagement.Api.Persistence.Configurations;
using Aperia.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aperia.CaseManagement.Api.Persistence;

public partial class CaseManagementContext
{
    public virtual DbSet<Inquiry> Inquiries { get; set; } = null!;

    public virtual DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InquiryConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }
}