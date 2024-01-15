namespace ecommerce.API.Models.PaymentController
{
    public class InitiatePaymentModel
    {
        public string AddressTitle { get; set; } = null!;
        public string CurrencyCode { get; set; } = null!;
        public List<Item> Items { get; set; } = null!;

        public class Item
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
