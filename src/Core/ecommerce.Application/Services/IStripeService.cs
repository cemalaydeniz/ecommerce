using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using Stripe;

namespace ecommerce.Application.Services
{
    public interface IStripeService
    {
        Event ConstructEvent(string body, string header);
        PaymentIntent CreatePaymentIntent(User user, UserAddress address, decimal totalAmount, string currenyCode, Dictionary<string, string> items);
    }
}
