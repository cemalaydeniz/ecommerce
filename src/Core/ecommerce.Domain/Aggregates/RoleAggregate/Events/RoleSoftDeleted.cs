using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.RoleAggregate.Events
{
    public record RoleSoftDeleted(Role SoftDeletedRole) : IDomainEvent;
}
