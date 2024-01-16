using ecommerce.Domain.Aggregates.OrderAggregate;

namespace ecommerce.Application.Features.Queries.GetMyOrders
{
    public class GetMyOrdersQueryResponse
    {
        public List<Order> Orders { get; set; } = null!;
    }
}
