using InternetShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetShop.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Products)
                .WithOne(e => e.Category);

            builder.HasMany(e => e.Categories)
                .WithOne(e => e.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Title)
                .HasMaxLength(Category.MAX_TITLE_LENGHT)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(Category.MAX_DESCRIPTION_LENGHT)
                .IsRequired(false);
        }
    }
}
