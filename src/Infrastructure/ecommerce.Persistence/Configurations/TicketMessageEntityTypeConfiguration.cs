using ecommerce.Domain.Aggregates.OrderRepository.Entities;
using ecommerce.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ecommerce.Persistence.Configurations
{
    public class TicketMessageEntityTypeConfiguration : IEntityTypeConfiguration<TicketMessage>
    {
        public void Configure(EntityTypeBuilder<TicketMessage> builder)
        {
            // Primary key
            builder.HasKey(tm => tm.Id);

            // Required fields
            builder.Property(tm => tm.Content)
                .IsRequired()
                .HasMaxLength(TicketMessage.ContentMaxLength);

            builder.Property(tm => tm.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("timezone('utc', now())")
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // Relations
            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<TicketMessage>(tm => tm.UserId)
                .IsRequired();
        }
    }
}
