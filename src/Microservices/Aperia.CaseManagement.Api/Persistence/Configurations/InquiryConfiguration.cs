using Aperia.CaseManagement.Api.Entities;
using Aperia.Core.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperia.CaseManagement.Api.Persistence.Configurations
{
    internal sealed class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> entity)
        {
            entity.ToTable("Inquiry");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasConversion<UtcDateConverter>();
            entity.Property(e => e.EntityId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OwnerType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasConversion<EnumValueConverter<OwnerType>>();
            entity.Property(e => e.SecondaryStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasConversion<EnumValueConverter<InquiryStatus>>();
            entity.Property(e => e.StatusDate)
                .HasColumnType("datetime")
                .HasConversion<UtcDateConverter>();
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasConversion<UtcDateConverter>();
        }
    }
}