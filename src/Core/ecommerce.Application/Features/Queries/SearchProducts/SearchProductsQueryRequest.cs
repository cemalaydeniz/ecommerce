using MediatR;

namespace ecommerce.Application.Features.Queries.SearchProducts
{
    public class SearchProductsQueryRequest : IRequest<SearchProductsQueryResponse>
    {
        public string Name { get; set; } = null!;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
