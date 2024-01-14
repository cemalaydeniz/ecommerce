namespace ecommerce.Application.Models.ValueObjects
{
    public class MoneyModel
    {
        public string CurrencyCode { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
