using RusGold.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RusGold.Data.Concrete.EntityFramework.Mappings
{
    public class RegistersMap : IEntityTypeConfiguration<Registers>
    {
        public void Configure(EntityTypeBuilder<Registers> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            
            builder.Property(p => p.Fullname);
            builder.Property(p => p.Email);
            builder.Property(p => p.Number);
            builder.Property(p => p.Message);
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();
            builder.Property(p => p.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();

            builder.ToTable("Registers");
        }
    }
}
