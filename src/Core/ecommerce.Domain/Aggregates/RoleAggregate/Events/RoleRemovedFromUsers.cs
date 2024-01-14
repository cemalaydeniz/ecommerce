using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.RoleAggregate.Events
{
    public record RoleRemovedFromUsers(Role RemovedRole) : IDomainEvent;
}
