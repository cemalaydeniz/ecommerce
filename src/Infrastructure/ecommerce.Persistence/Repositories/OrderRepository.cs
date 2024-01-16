using ecommerce.Domain.Aggregates.OrderAggregate;
using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order newOrder, CancellationToken cancellationToken)
        {
            await _dbContext.Orders.AddAsync(newOrder, cancellationToken);
        }

        public async Task AddTicketMessageAsync(Order order, TicketMessage ticketMessage, CancellationToken cancellationToken)
        {
            Update(order);
            await _dbContext.TicketMessages.AddAsync(ticketMessage);
        }

        public void Update(Order order)
        {
            _dbContext.Orders.Update(order);
        }

        public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.TicketMessages)
                .FirstOrDefaultAsync(o => o.Id.Equals(orderId), cancellationToken);
        }

        public async Task<List<Order>> GetOrdersofUserAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Where(o => o.UserId.Equals(userId))
                .OrderByDescending(o => o.CreatedAt)
                .Include(o => o.OrderItems)
                .Include(o => o.TicketMessages)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Order>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .OrderByDescending(o => o.CreatedAt)
                .Include(o => o.OrderItems)
                .Include(o => o.TicketMessages)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
