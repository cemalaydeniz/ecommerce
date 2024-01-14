using ecommerce.Domain.Aggregates.OrderRepository;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.OrderRepository.Enums;
using ecommerce.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ecommerce.Persistence.Configurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Primary key
            builder.HasKey(o => o.Id);

            // Required fields
            builder.Property(o => o.UserName)
                .IsRequired()
                .HasMaxLength(User.NameMaxLength);

            builder.Property(o => o.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("timezone('utc', now())")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(o => o.TicketStatus)
                .HasColumnType("varchar(15)")
                .IsRequired()
                .HasConversion(app => app.ToString(), db => (TicketStatus)Enum.Parse(typeof(TicketStatus), db));

            // Value objects
            builder.OwnsOne(o => o.DeliveryAddress, address =>
            {
                address.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(Address.StreetMaxLength);

                address.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(Address.ZipCodeMaxLength);

                address.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(Address.CityMaxLength);

                address.Property(a => a.Country)
                    .IsRequired()
                    .HasMaxLength(Address.CountryMaxLength);
            });

            // Relations
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .IsRequired();

            builder.HasMany(o => o.TicketMessages)
                .WithOne()
                .IsRequired(false);
        }
    }
}
