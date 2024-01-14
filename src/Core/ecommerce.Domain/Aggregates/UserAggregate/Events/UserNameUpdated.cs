using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events
{
    public record UserNameUpdated(User UpdatedUser, string? UpdatedName) : IDomainEvent;
}
