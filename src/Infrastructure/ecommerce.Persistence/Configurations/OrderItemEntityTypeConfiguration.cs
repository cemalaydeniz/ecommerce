using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Persistence.Configurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Primary key
            builder.HasKey(oi => oi.Id);

            // Required fields
            builder.Property(oi => oi.ProductName)
                .IsRequired()
                .HasMaxLength(Product.NameMaxLength);

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            // Value objects
            builder.OwnsOne(oi => oi.UnitPrice, money =>
            {
                money.Property(m => m.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(Money.CurrenyCodeMaxLength);

                money.Property(m => m.Amount)
                    .IsRequired()
                    .HasColumnType($"decimal({Money.AmountMaxDigitBeforeComma},{Money.AmountMaxDigitAfterComma})");
            });

            // Relations
            builder.HasOne<Product>()
                .WithOne()
                .HasForeignKey<OrderItem>(oi => oi.ProductId)
                .IsRequired();
        }
    }
}
