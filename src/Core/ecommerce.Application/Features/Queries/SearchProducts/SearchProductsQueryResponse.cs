using ecommerce.Domain.Aggregates.ProductAggregate;

namespace ecommerce.Application.Features.Queries.SearchProducts
{
    public class SearchProductsQueryResponse
    {
        public List<Product> Products { get; set; } = null!;
    }
}
