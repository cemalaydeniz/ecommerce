using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository.Events
{
    public record OrderTicketClosed(Order UpdatedOrder) : IDomainEvent;
}
