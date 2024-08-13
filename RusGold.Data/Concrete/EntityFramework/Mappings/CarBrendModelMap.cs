using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RusGold.Entities.Concrete;

namespace RusGold.Data.Concrete.EntityFramework.Mappings
{
    public class CarBrendModelMap : IEntityTypeConfiguration<CarBrendModel>
    {
        public void Configure(EntityTypeBuilder<CarBrendModel> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(c => c.ParentId)
                .HasDefaultValue(0);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.CreatedDate)
                .IsRequired();

            builder.Property(c => c.ModifiedDate)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();

            builder.Property(c => c.IsActive)
                .IsRequired();

            builder.Property(c => c.CreatedByName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.ModifiedByName)
                .IsRequired()
                .HasMaxLength(50);

            builder.ToTable("CarBrendModels");
        }
    }
}
