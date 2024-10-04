using InternetShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetShop.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.Product)
                .WithMany(e => e.Items);

            builder.HasOne(e => e.Order)
                .WithMany(e => e.Items);

            builder.Property(p => p.Price)
                 .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(p => p.Count).IsRequired();
        }
    }
}
