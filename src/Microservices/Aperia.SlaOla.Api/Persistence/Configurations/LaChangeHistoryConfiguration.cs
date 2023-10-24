using Aperia.SlaOla.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperia.SlaOla.Api.Persistence.Configurations
{
    internal sealed class LaChangeHistoryConfiguration : IEntityTypeConfiguration<LaChangeHistory>
    {
        public void Configure(EntityTypeBuilder<LaChangeHistory> entity)
        {
            entity.ToTable("LaChangeHistory");

            entity.Property(e => e.Attribute)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(250);

            entity.HasOne(d => d.LaObject).WithMany(p => p.LaChangeHistories)
                .HasForeignKey(d => d.LaObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LaChangeHistory_LaObject");
        }
    }
}