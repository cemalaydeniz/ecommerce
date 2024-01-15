using ecommerce.Application.Services;
using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Queries.InitiatePayment
{
    public class InitiatePaymentQueryHandler : IRequestHandler<InitiatePaymentQueryRequest, ValidationBehaviorResult<InitiatePaymentQueryResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IStripeService _stripeService;

        public InitiatePaymentQueryHandler(IUnitofWork unitofWork, IStripeService stripeService)
        {
            _unitofWork = unitofWork;
            _stripeService = stripeService;
        }

        public async Task<ValidationBehaviorResult<InitiatePaymentQueryResponse>> Handle(InitiatePaymentQueryRequest request, CancellationToken cancellationToken)
        {
            User? user = await _unitofWork.UserRepository.GetByIdAsync(request.UserId, false, false, cancellationToken);
            if (user == null)
                return ValidationBehaviorResult<InitiatePaymentQueryResponse>.Fail(ConstantsUtility.User.UserNotFound);

            if (user.Name == null)
                return ValidationBehaviorResult<InitiatePaymentQueryResponse>.Fail(ConstantsUtility.Payment.UserNameRequired);

            UserAddress? address = user.Addresses.FirstOrDefault(a => a.Title == request.AddressTitle);
            if (address == null)
                return ValidationBehaviorResult<InitiatePaymentQueryResponse>.Fail(ConstantsUtility.Payment.AddressRequired);

            if (request.Items.Count != request.Items.GroupBy(x => x.ProductId).Count())
                return ValidationBehaviorResult<InitiatePaymentQueryResponse>.Fail(ConstantsUtility.Payment.RepeatedProduct);

            var productIds = request.Items.Select(i => i.ProductId).ToList();
            var products = await _unitofWork.ProductRepository.GetByIdsAsync(productIds, false, cancellationToken);
            if (products.Count == 0 || products.Count != productIds.Count)
                return ValidationBehaviorResult<InitiatePaymentQueryResponse>.Fail(ConstantsUtility.Payment.ProductsNotFound);

            var quantities = request.Items.ToDictionary(_ => _.ProductId, _ => _.Quantity);
            long totalAmount = 0;
            var items = new Dictionary<string, string>();
            foreach (var product in products)
            {
                Money? price = product.Prices.FirstOrDefault(m => m.CurrencyCode == request.CurrencyCode);
                if (price == null)
                    return ValidationBehaviorResult<InitiatePaymentQueryResponse>.Fail(ConstantsUtility.Payment.ProductsCurrencyCodeDifferent);

                long priceAmount = (long)(price.Amount * 100);
                totalAmount += priceAmount;
                items.Add(product.Id.ToString() + ConstantsUtility.Payment.ItemDataSeperator + product.Name,
                    priceAmount + ConstantsUtility.Payment.ItemDataSeperator + quantities[product.Id]);
            }

            var paymentIntent = _stripeService.CreatePaymentIntent(user, address, totalAmount, request.CurrencyCode, items);

            return new InitiatePaymentQueryResponse()
            {
                PaymentIntent = paymentIntent
            };
        }
    }
}
