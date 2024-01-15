using ecommerce.Application.Services;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace ecommerce.Infrastructure.Stripe
{
    public class StripeService : IStripeService
    {
        private readonly IConfiguration _configuration;

        public StripeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Event ConstructEvent(string body, string header)
        {
            return EventUtility.ConstructEvent(body, header, _configuration[StripeConstants.SecretKey_ConfigKey]);
        }

        public PaymentIntent CreatePaymentIntent(User user,
            UserAddress address,
            decimal totalAmount,
            string currenyCode,
            Dictionary<string, string> metaData)
        {
            StripeConfiguration.ApiKey = _configuration[StripeConstants.SecretKey_ConfigKey];

            metaData.Add(nameof(User.Id), user.Id.ToString());

            var paymentIntent = new PaymentIntentService().Create(new PaymentIntentCreateOptions()
            {
                Amount = (long)(totalAmount * 100),
                Currency = currenyCode,
                Metadata = metaData,
                ReceiptEmail = user.Email!.ToString(),
                Shipping = new ChargeShippingOptions()
                {
                    Name = user.Name,
                    Phone = user.PhoneNumber,
                    Address = new AddressOptions()
                    {
                        Line1 = address.Address.Street,
                        PostalCode = address.Address.ZipCode,
                        City = address.Address.City,
                        Country = address.Address.Country
                    }
                },
            });

            return paymentIntent;
        }
    }
}
