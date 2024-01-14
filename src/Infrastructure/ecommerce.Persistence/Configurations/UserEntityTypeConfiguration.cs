using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ecommerce.Persistence.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes
            builder.HasIndex(u => u.Email)          // To speed up user search by their email addresses
                .IsUnique();

            // Required fields
            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("timezone('utc', now())")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(u => u.IsDeleted)
                .IsRequired();

            // Nullable fields
            builder.Property(u => u.Email)
                .IsRequired(false)                  // To comply with GDPR
                .IsUnicode(false)
                .HasMaxLength(User.EmailMaxLength)
                .HasConversion(app => app == null ? null : app.ToLower(), db => db);

            builder.Property(u => u.PasswordHashed)
                .IsRequired(false);                 // To comply with GDPR

            builder.Property(u => u.Name)
                .IsRequired(false)
                .HasMaxLength(User.NameMaxLength);

            builder.Property(u => u.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(User.PhoneNumberMaxLength);

            // Value objects
            builder.OwnsMany(u => u.Addresses, userAddress =>
            {
                userAddress.Property(ua => ua.Title)
                    .IsRequired()
                    .HasMaxLength(UserAddress.TitleMaxLength);

                userAddress.OwnsOne(ua => ua.Address, address =>
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
            });
        }
    }
}
