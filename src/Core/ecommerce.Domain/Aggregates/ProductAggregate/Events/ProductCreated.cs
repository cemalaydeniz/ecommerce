using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events
{
    public record ProductCreated(Product CreatedProduct) : IDomainEvent;
}
