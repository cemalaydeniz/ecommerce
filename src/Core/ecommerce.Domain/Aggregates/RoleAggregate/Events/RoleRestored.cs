using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.RoleAggregate.Events
{
    public record RoleRestored(Role RestoredRole) : IDomainEvent;
}
