using Aperia.SlaOla.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperia.SlaOla.Api.Persistence.Configurations
{
    /// <summary>
    /// The LaObject Configuration
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{LaObject}" />
    public class LaObjectConfiguration : IEntityTypeConfiguration<LaObject>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Configure(EntityTypeBuilder<LaObject> entity)
        {
            entity.ToTable("LaObject");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Source)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}