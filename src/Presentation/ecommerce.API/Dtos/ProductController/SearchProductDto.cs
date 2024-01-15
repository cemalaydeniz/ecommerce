using ecommerce.Domain.Common.ValueObjects;

namespace ecommerce.API.Dtos.ProductController
{
    public class SearchProductDto
    {
        public List<ProductInfo> Result { get; set; } = null!;

        public class ProductInfo
        {
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public List<Money> Prices { get; set; } = null!;
        }
    }
}
