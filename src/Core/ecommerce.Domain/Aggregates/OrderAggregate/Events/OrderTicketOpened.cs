using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderAggregate.Events
{
    public record OrderTicketOpened(Order UpdatedOrder) : IDomainEvent;
}
