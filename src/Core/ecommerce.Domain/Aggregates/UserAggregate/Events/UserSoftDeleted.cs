using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events
{
    public record UserSoftDeleted(Guid SoftDeletedUserId) : IDomainEvent;
}
