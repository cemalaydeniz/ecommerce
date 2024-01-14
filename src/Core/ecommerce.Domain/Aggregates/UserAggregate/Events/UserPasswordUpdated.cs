using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events
{
    public record UserPasswordUpdated(User UpdatedUser) : IDomainEvent;
}
