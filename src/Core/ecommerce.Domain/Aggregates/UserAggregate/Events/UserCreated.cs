using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events
{
    public record UserCreated(User CreatedUser) : IDomainEvent;
}
