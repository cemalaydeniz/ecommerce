using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events
{
    public record ProductSoftDeleted(Product DeletedProduct) : IDomainEvent;
}
