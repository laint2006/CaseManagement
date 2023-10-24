using Aperia.Core.Persistence.ValueConverters;
using Aperia.Ownership.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperia.Ownership.Api.Persistence.Configurations
{
    public partial class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> entity)
        {
            entity.ToTable("Owner");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.OwnerType)
            .HasMaxLength(30)
            .IsUnicode(false)
            .HasConversion<EnumValueConverter<OwnerType>>();
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}