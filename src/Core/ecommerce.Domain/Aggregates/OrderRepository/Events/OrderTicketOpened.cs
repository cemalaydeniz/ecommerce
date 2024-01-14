using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository.Events
{
    public record OrderTicketOpened(Order UpdatedOrder) : IDomainEvent;
}
