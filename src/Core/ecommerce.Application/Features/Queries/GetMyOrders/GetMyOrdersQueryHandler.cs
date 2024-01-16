using ecommerce.Application.UnitofWorks;
using MediatR;

namespace ecommerce.Application.Features.Queries.GetMyOrders
{
    public class GetMyOrdersQueryHandler : IRequestHandler<GetMyOrdersQueryRequest, GetMyOrdersQueryResponse>
    {
        public const int PageSize = 10;

        private readonly IUnitofWork _unitofWork;

        public GetMyOrdersQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<GetMyOrdersQueryResponse> Handle(GetMyOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            return new GetMyOrdersQueryResponse()
            {
                Orders = await _unitofWork.OrderRepository.GetOrdersofUserAsync(request.UserId, request.Page, PageSize, cancellationToken)
            };
        }
    }
}
