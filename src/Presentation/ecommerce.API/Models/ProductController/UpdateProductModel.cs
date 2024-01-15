using ecommerce.Application.Models.ValueObjects;

namespace ecommerce.API.Models.ProductController
{
    public class UpdateProductModel
    {
        public string? NewName { get; set; }
        public string? NewDescription { get; set; }
        public List<string>? CurrencyCodesToRemove { get; set; }
        public List<MoneyModel>? PricesToUpdateOrAdd { get; set; }
    }
}
