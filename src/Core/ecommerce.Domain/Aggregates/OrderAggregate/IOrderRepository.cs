using ecommerce.Domain.Aggregates.OrderAggregate.Entities;

namespace ecommerce.Domain.Aggregates.OrderAggregate
{
    /// <summary>
    /// Available CRUD operations for the aggregate root: <see cref="Order"/>
    /// </summary>
    public interface IOrderRepository
    {
        Task AddAsync(Order newOrder, CancellationToken cancellationToken = default);
        Task AddTicketMessageAsync(Order order, TicketMessage ticketMessage, CancellationToken cancellationToken = default);
        void Update(Order order);
        Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<List<Order>> GetOrdersofUserAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken = default);
        Task<List<Order>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
