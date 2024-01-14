using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events
{
    public record ProductNameUpdated(Product UpdatedProduct, string UpdatedName) : IDomainEvent;
}
