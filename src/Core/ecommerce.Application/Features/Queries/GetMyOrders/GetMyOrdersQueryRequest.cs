using MediatR;

namespace ecommerce.Application.Features.Queries.GetMyOrders
{
    public class GetMyOrdersQueryRequest : IRequest<GetMyOrdersQueryResponse>
    {
        public Guid UserId { get; set; }
        public int Page { get; set; }
    }
}
