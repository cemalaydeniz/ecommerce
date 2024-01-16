using ecommerce.Domain.Aggregates.OrderRepository;

namespace ecommerce.Application.Features.Queries.GetMyOrders
{
    public class GetMyOrdersQueryResponse
    {
        public List<Order> Orders { get; set; } = null!;
    }
}
