using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RusGold.Entities.Concrete;

namespace RusGold.Data.Concrete.EntityFramework.Mappings
{
    public class CarMap : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

                builder.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(350);

                builder.Property(c => c.Price)
                    .HasMaxLength(50);

                builder.Property(c => c.HorsePower)
                    .HasMaxLength(50);

                builder.Property(c => c.Content)
                    .IsRequired();

                builder.Property(c => c.TechnicalParameters);

                builder.Property(c => c.ThumbNail)
                    .IsRequired()
                    .HasMaxLength(300);

                builder.Property(c => c.UserId)
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

                builder.Property(c => c.Body);
                    
                builder.Property(c => c.FuelType)
                    .HasMaxLength(50);

                builder.Property(c => c.Year)
                    .HasMaxLength(50);

                builder.Property(c => c.Transmission)
                    .HasMaxLength(50);

                builder.Property(c => c.DriveType)
                    .HasMaxLength(50);

                builder.Property(c => c.ModelId).IsRequired(); ;

                builder.Property(c => c.BrendId).IsRequired(); ;

            builder.Property(c => c.Color)
                    .HasMaxLength(50);

                builder.Property(c => c.EngineSize)
                    .HasMaxLength(50);
            }
        }
    }

