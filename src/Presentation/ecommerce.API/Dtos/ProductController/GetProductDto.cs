using ecommerce.Domain.Common.ValueObjects;

namespace ecommerce.API.Dtos.ProductController
{
    public class GetProductDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<Money> Prices { get; set; } = null!;
    }
}
