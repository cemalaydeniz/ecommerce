using MediatR;

namespace ecommerce.Application.Features.Queries.GetOrders
{
    public class GetOrdersQueryRequest : IRequest<GetOrdersQueryResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
