using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events
{
    public record UserAddressAdded(User UpdatedUser, UserAddress NewAddress) : IDomainEvent;
}
