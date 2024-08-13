using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RusGold.Entities.Concrete;

namespace RusGold.Data.Concrete.EntityFramework.Mappings
{
    public class CreditMap : IEntityTypeConfiguration<Credits>
    {
        public void Configure(EntityTypeBuilder<Credits> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.ToTable("Credits");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(c => c.ModelId)
                .IsRequired();

            builder.Property(c => c.Period)
                .IsRequired();

            builder.Property(c => c.MonthlyPay)
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(c => c.CarPrice)
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(c => c.InitialPayment)
                .HasColumnType("decimal")
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();

            builder.Property(c => c.IsActive)
                .IsRequired();
        }
        }
    }

