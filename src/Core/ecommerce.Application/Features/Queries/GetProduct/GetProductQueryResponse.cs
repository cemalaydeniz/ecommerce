using ecommerce.Domain.Aggregates.ProductAggregate;

namespace ecommerce.Application.Features.Queries.GetProduct
{
    public class GetProductQueryResponse
    {
        public Product Product { get; set; } = null!;
    }
}
