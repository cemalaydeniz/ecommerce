using ecommerce.Domain.Aggregates.OrderAggregate;

namespace ecommerce.Application.Features.Queries.GetOrders
{
    public class GetOrdersQueryResponse
    {
        public List<Order> Orders { get; set; } = null!;
    }
}
