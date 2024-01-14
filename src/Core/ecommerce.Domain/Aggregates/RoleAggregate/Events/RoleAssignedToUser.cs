using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.RoleAggregate.Events
{
    public record RoleAssignedToUser(Role AssignedRole, User UpdatedUser) : IDomainEvent;
}
