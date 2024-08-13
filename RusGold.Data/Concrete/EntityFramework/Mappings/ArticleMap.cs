using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();//Identity

            builder.Property(a => a.Title).HasMaxLength(150).IsRequired();
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.SeoAuthor).HasMaxLength(60).IsRequired();
            builder.Property(a => a.SeoDescription).HasMaxLength(300).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(70).IsRequired();
            builder.Property(a => a.ThumbNail).HasMaxLength(300).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();

            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();

            builder.HasOne<User>(u => u.User).WithMany(u => u.Articles).HasForeignKey(
                u => u.UserId);

            builder.ToTable("Articles");
        }
    }
}
