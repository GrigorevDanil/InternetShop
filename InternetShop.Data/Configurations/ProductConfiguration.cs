using InternetShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetShop.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Brand)
                .WithMany(e => e.Products);

            builder.HasOne(e => e.Category)
                .WithMany(e => e.Products);

            builder.HasMany(e => e.Items)
                .WithOne(e => e.Product);

            builder.Property(p => p.Title)
                .HasMaxLength(Product.MAX_TITLE_LENGHT)
                .IsRequired();

            builder.Property(p => p.Description)
               .HasMaxLength(Product.MAX_DESCRIPTION_LENGHT)
               .IsRequired();

            builder.Property(p => p.Price)
                 .HasPrecision(10, 2)
                 .IsRequired();

            builder.Property(p => p.Count).IsRequired();

            builder.ComplexProperty(p => p.MainPhoto, b =>
            {
                b.IsRequired();
                b.Property(p => p.Path).HasColumnName("path");
                b.Property(p => p.ContentType).HasColumnName("content_type");
            });
        }
    }
}
