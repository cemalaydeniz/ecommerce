using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.RoleAggregate.Events
{
    public record RoleNameUpdated(Role UpdatedRole, string UpdatedName) : IDomainEvent;
}
