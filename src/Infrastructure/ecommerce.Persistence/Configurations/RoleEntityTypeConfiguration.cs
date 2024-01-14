using ecommerce.Domain.Aggregates.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ecommerce.Persistence.Configurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Indexes
            builder.HasIndex(r => r.Name)       // To speed up role search by their names
                .IsUnique();

            // Required fields
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(Role.NameMaxLength);

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("timezone('utc', now())")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(r => r.LastUpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("timezone('utc', now())")
                .ValueGeneratedOnAddOrUpdate();

            // Relations
            builder.HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity(j => j.ToTable("UserRoles"));
        }
    }
}
