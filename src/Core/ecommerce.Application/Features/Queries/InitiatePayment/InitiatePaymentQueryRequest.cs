using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Queries.InitiatePayment
{
    public class InitiatePaymentQueryRequest : IRequest<ValidationBehaviorResult<InitiatePaymentQueryResponse>>
    {
        public Guid UserId { get; set; }
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
