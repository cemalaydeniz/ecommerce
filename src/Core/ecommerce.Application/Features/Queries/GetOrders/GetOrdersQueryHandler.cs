using ecommerce.Application.UnitofWorks;
using MediatR;

namespace ecommerce.Application.Features.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest, GetOrdersQueryResponse>
    {
        private readonly IUnitofWork _unitofWork;

        public GetOrdersQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<GetOrdersQueryResponse> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            return new GetOrdersQueryResponse()
            {
                Orders = await _unitofWork.OrderRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken)
            };
        }
    }
}
