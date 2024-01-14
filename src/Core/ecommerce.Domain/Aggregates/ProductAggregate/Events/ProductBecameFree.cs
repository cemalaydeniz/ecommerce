using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events
{
    public record ProductBecameFree(Product UpdatedProduct) : IDomainEvent;
}
