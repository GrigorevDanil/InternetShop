using InternetShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetShop.Infrastructure.Configurations.Write
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Products)
                .WithOne(e => e.Brand);

            builder.Property(p => p.Title)
                .HasMaxLength(Brand.MAX_TITLE_LENGHT)
                .IsRequired();
        }
    }
}
