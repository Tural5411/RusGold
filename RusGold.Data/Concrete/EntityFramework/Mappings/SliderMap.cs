using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RusGold.Entities.Concrete;

namespace RusGold.Data.Concrete.EntityFramework.Mappings
{
    public class SliderMap : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.ImageUrl).IsRequired();
            builder.Property(a => a.Name).HasColumnType("NVARCHAR(MAX)");
            builder.Property(a => a.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();

            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();

            builder.ToTable("Slider");
        }
    }
}
