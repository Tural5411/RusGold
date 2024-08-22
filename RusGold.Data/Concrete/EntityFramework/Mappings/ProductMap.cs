using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RusGold.Entities.Concrete;

namespace RusGold.Data.Concrete.EntityFramework.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(350);

            builder.Property(c => c.Price);

            builder.Property(c => c.PriceByCard);


            builder.Property(c => c.IsGold);
            builder.Property(c => c.Content)
                    .IsRequired();


            builder.Property(c => c.ThumbNail)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.CategoryId)
                .IsRequired();

            builder.Property(c => c.UnitId)
                .IsRequired();

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

            builder.ToTable("Products");

        }
    }
}

