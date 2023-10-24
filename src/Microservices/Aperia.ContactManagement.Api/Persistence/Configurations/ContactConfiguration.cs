using Aperia.ContactManagement.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperia.ContactManagement.Api.Persistence.Configurations
{
    internal sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> entity)
        {
            entity.ToTable("Contact");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber)
            .HasMaxLength(20)
            .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}