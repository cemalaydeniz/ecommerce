using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events
{
    public record UserEmailUpdated(User UpdatedUser, string UpdatedEmail) : IDomainEvent;
}
