using ecommerce.Application.Services;
using ecommerce.Application.UnitofWorks;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Application.Validations.Behaviors;
using ecommerce.Domain.Aggregates.OrderAggregate;
using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Common.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Features.Commands.PaymentCheck
{
    public class PaymentCheckCommandHandler : IRequestHandler<PaymentCheckCommandRequest, ValidationBehaviorResult<PaymentCheckCommandResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IStripeService _stripeService;
        private readonly ILogger<PaymentCheckCommandHandler> _logger;

        public PaymentCheckCommandHandler(IUnitofWork unitofWork,
            IStripeService stripeService,
            ILogger<PaymentCheckCommandHandler> logger)
        {
            _unitofWork = unitofWork;
            _stripeService = stripeService;
            _logger = logger;
        }

        public async Task<ValidationBehaviorResult<PaymentCheckCommandResponse>> Handle(PaymentCheckCommandRequest request, CancellationToken cancellationToken)
        {
            var stripeEvent = _stripeService.ConstructEvent(request.Body, request.Header);
            var paymentIntent = (Stripe.PaymentIntent)stripeEvent.Data.Object;
            switch (stripeEvent.Type)
            {
                case Stripe.Events.PaymentIntentSucceeded:
                    try
                    {
                        var orderId = await CreateOrder(paymentIntent, cancellationToken);
                        return new PaymentCheckCommandResponse()
                        {
                            OrderId = orderId
                        };
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical($"An order could not be saved to Db: {e.Message}");
                        /**
                         * In case something happens while creating an order, the error can be catched here and the payment data
                         * can be saved somewhere else to handle later, because if this error occurs, it means, that the payment
                         * is succeeded and the customer has been charged, but could not add the order to the Db
                         */
                    }
                    break;
                    // The other events can be handled here
            }

            return new PaymentCheckCommandResponse();
        }

        private async Task<Guid> CreateOrder(Stripe.PaymentIntent paymentIntent, CancellationToken cancellationToken)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in paymentIntent.Metadata)
            {
                if (item.Key == nameof(User.Id))
                    continue;

                string[] key = item.Key.Split(ConstantsUtility.Payment.ItemDataSeperator);
                string[] value = item.Value.Split(ConstantsUtility.Payment.ItemDataSeperator);

                Guid productId = Guid.Parse(key[0]);
                string productName = key[1];
                long unitPriceAmount = long.Parse(value[0]);
                int quantity = int.Parse(value[1]);

                orderItems.Add(new OrderItem(productId,
                    productName,
                    new Money(paymentIntent.Currency, (decimal)unitPriceAmount / 100),
                    quantity));
            }

            Order newOrder = new Order(Guid.Parse(paymentIntent.Metadata[nameof(User.Id)]),
                paymentIntent.Shipping.Name,
                new Address(paymentIntent.Shipping.Address.Line1,
                    paymentIntent.Shipping.Address.PostalCode,
                    paymentIntent.Shipping.Address.City,
                    paymentIntent.Shipping.Address.Country),
                orderItems);

            await _unitofWork.OrderRepository.AddAsync(newOrder, cancellationToken);
            await _unitofWork.SaveChangesAsync(cancellationToken);

            return newOrder.Id;
        }
    }
}
