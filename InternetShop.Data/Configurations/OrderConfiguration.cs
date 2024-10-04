using InternetShop.Domain.Entities;
using InternetShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetShop.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Items)
                .WithOne(e => e.Order);

            builder.HasOne(e => e.User)
                .WithMany(e => e.Orders);

            builder.Property(p => p.TotalAmount)
                .HasPrecision(10,2)
                .IsRequired();

            builder.ComplexProperty(p => p.OrderStatuses, b =>
            {
                b.IsRequired();
                b.Property(p => p.Value)
                    .HasMaxLength(OrderStatuses.MAX_STATUS_LENGHT)
                    .HasColumnName("OrderStatus");
            });

            builder.ComplexProperty(p => p.PaymentStatuses, b =>
            {
                b.IsRequired();
                b.Property(p => p.Value)
                    .HasMaxLength(PaymentStatuses.MAX_STATUS_LENGHT)
                    .HasColumnName("PaymentStatus");
            });

            builder.Property(p => p.DatePayment).IsRequired();
        }
    }
}
