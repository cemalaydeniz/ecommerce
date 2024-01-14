using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.RoleAggregate.Events
{
    public record RoleRemovedFromUser(Role RemovedRole, User UpdatedUser) : IDomainEvent;
}
