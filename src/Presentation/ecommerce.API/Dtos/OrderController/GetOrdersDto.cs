using ecommerce.Domain.Common.ValueObjects;

namespace ecommerce.API.Dtos.OrderController
{
    public class GetOrdersDto
    {
        public List<OrderInfo> Orders { get; set; } = null!;

        public class OrderInfo
        {
            public string UserName { get; set; } = null!;
            public Address DeliveryAddress { get; set; } = null!;
            public DateTime CreatedAt { get; set; }
            public List<ItemInfo> Items { get; set; } = null!;

            public class ItemInfo
            {
                public string ProductName { get; set; } = null!;
                public Money UnitPrice { get; set; } = null!;
                public int Quantity { get; set; }
            }
        }
    }
}
