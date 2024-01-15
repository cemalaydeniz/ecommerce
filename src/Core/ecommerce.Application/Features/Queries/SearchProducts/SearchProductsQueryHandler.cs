using ecommerce.Application.UnitofWorks;
using MediatR;

namespace ecommerce.Application.Features.Queries.SearchProducts
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQueryRequest, SearchProductsQueryResponse>
    {
        private const int PageSize = 10;    // There can be an enum to get the prefered page size from the user

        private readonly IUnitofWork _unitofWork;

        public SearchProductsQueryHandler(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<SearchProductsQueryResponse> Handle(SearchProductsQueryRequest request, CancellationToken cancellationToken)
        {
            return new SearchProductsQueryResponse()
            {
                Products = await _unitofWork.ProductRepository.SearchByNameAsync(request.Name, request.Page, PageSize, false, cancellationToken)
            };
        }
    }
}
