using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events
{
    public record ProductPriceUpdated(Product UpdatedProduct, Money UpdatedPrice) : IDomainEvent;
}
