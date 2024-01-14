using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository.Events
{
    public record OrderCreated(Order CreatedOrder) : IDomainEvent;
}
