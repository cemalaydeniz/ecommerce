using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderAggregate.Events
{
    public record OrderCreated(Order CreatedOrder) : IDomainEvent;
}
