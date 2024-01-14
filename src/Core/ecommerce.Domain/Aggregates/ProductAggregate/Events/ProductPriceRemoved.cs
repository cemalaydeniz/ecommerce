using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events
{
    public record ProductPriceRemoved(Product UpdatedProduct, Money RemovedPrice) : IDomainEvent;
}
