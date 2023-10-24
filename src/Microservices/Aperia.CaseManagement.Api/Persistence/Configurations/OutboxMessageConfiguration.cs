using Aperia.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperia.CaseManagement.Api.Persistence.Configurations
{
    /// <summary>
    /// The Outbox Message Configuration
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{OutboxMessage}" />
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Configure(EntityTypeBuilder<OutboxMessage> entity)
        {
            entity.ToTable("OutboxMessage");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EntityId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.EventType)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.Payload).IsRequired();
            entity.Property(e => e.ProcessedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}