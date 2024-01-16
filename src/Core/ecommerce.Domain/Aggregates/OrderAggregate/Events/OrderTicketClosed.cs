using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderAggregate.Events
{
    public record OrderTicketClosed(Order UpdatedOrder) : IDomainEvent;
}
