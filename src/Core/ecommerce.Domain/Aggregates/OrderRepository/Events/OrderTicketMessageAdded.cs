using ecommerce.Domain.Aggregates.OrderRepository.Entities;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository.Events
{
    public record OrderTicketMessageAdded(Order UpdatedOrder, TicketMessage NewTicketMessage) : IDomainEvent;
}
