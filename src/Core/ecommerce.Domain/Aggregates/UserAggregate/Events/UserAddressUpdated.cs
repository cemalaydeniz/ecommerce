using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events
{
    public record UserAddressUpdated(User UpdatedUser, string TitleofUpdatedAddress, UserAddress UpdatedAddress) : IDomainEvent;
}
