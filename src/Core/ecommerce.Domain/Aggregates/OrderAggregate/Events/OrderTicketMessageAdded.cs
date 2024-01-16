using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderAggregate.Events
{
    public record OrderTicketMessageAdded(Order UpdatedOrder, TicketMessage NewTicketMessage) : IDomainEvent;
}
