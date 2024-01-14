using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ecommerce.Persistence.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Primary key
            builder.Property(p => p.Id);

            // Indexes
            builder.HasIndex(p => p.Name);          // To speed up product search by their names

            // Required fields
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Product.NameMaxLength);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Product.DescriptionMaxLength);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("timezone('utc', now())")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(p => p.IsDeleted)
                .IsRequired();

            // Value objects
            builder.OwnsMany(p => p.Prices, money =>
            {
                money.Property(m => m.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(Money.CurrenyCodeMaxLength);

                money.Property(m => m.Amount)
                    .IsRequired()
                    .HasColumnType($"decimal({Money.AmountMaxDigitBeforeComma},{Money.AmountMaxDigitAfterComma})");
            });
        }
    }
}
