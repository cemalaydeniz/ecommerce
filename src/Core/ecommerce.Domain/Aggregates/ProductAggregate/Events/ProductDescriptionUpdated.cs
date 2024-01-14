using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events
{
    public record ProductDescriptionUpdated(Product UpdatedProduct, string UpdatedDescription) : IDomainEvent;
}
