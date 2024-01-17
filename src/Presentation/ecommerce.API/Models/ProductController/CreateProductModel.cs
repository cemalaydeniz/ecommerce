using ecommerce.Application.Models.ValueObjects;

namespace ecommerce.API.Models.ProductController
{
    public class CreateProductModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<MoneyModel> Prices { get; set; } = null!;
    }
}
