using ecommerce.Domain.Aggregates.OrderRepository;

namespace ecommerce.Application.Features.Queries.GetOrders
{
    public class GetOrdersQueryResponse
    {
        public List<Order> Orders { get; set; } = null!;
    }
}
