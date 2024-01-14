using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events
{
    public record UserPhoneNumberUpdated(User UpdatedUser, string? UpdatedPhoneNumber) : IDomainEvent;
}
