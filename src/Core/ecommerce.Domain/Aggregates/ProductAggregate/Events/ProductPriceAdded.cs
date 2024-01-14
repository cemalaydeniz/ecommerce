using ecommerce.Domain.Common.ValueObjects;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events
{
    public record ProductPriceAdded(Product UpdatedProduct, Money NewPrice) : IDomainEvent;
}
