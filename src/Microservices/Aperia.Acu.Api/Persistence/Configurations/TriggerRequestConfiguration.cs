using Aperia.Acu.Api.Entities;
using Aperia.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperia.Acu.Api.Persistence.Configurations
{
    /// <summary>
    /// The Trigger Request Configuration
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{TriggerRequest}" />
    public class TriggerRequestConfiguration : IEntityTypeConfiguration<TriggerRequest>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Configure(EntityTypeBuilder<TriggerRequest> entity)
        {
            entity.ToTable("TriggerRequest");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EventType)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TriggerPointCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}