using Stripe;

namespace ecommerce.Application.Features.Queries.InitiatePayment
{
    public class InitiatePaymentQueryResponse
    {
        public PaymentIntent PaymentIntent { get; set; } = null!;
    }
}
