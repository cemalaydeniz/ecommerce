using ecommerce.Application.Features.Commands.PaymentCheck;
using ecommerce.Application.Services;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;
using ecommerce.TestUtility;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.ApplicationUnitTest.Features.Commands.PaymentCheck
{
    public class PaymentCheckCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly Mock<IStripeService> _mockStripeService;
        private readonly Mock<ILogger<PaymentCheckCommandHandler>> _mockLogger;

        private PaymentCheckCommandHandler _paymentCheckCommandHandler;

        public PaymentCheckCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _mockStripeService = new Mock<IStripeService>();
            _mockLogger = new Mock<ILogger<PaymentCheckCommandHandler>>();

            _paymentCheckCommandHandler = new PaymentCheckCommandHandler(_unitofWorkFixture.UnitofWork,
                _mockStripeService.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async void PaymentCheckCommandHandler_WhenReceiveSuccessEventFromStripe_ShouldCreateOrder()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            _mockStripeService.Setup(m => m.ConstructEvent(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Stripe.Event()
                {
                    Type = Stripe.Events.PaymentIntentSucceeded,
                    Data = new Stripe.EventData()
                    {
                        Object = new Stripe.PaymentIntent()
                        {
                            Amount = (long)(newProduct.Prices[0].Amount * 100),
                            Currency = MoneyTestUtility.ValidCurrencyCode,
                            Metadata = new Dictionary<string, string>()
                            {
                                { nameof(User.Id), newUser.Id.ToString() },
                                {
                                    newProduct.Id + Application.Utilities.Constants.ConstantsUtility.Payment.ItemDataSeperator + newProduct.Name,
                                    newProduct.Prices[0].Amount + Application.Utilities.Constants.ConstantsUtility.Payment.ItemDataSeperator + OrderItemTestUtility.ValidQuantity
                                }
                            },
                            ReceiptEmail = newUser.Email!.ToString(),
                            Shipping = new Stripe.Shipping()
                            {
                                Name = newUser.Name,
                                Phone = newUser.PhoneNumber,
                                Address = new Stripe.Address()
                                {
                                    Line1 = newUser.Addresses[0].Address.Street,
                                    PostalCode = newUser.Addresses[0].Address.ZipCode,
                                    City = newUser.Addresses[0].Address.City,
                                    Country = newUser.Addresses[0].Address.Country
                                }
                            },
                        }
                    }
                });

            var request = new PaymentCheckCommandRequest()
            {
                Body = StringGenerator.GenerateRandomStringStrictLength(10),
                Header = StringGenerator.GenerateRandomStringStrictLength(10)
            };

            // Act
            var result = await _paymentCheckCommandHandler.Handle(request, default);
            var order = await _unitofWorkFixture.UnitofWork.OrderRepository.GetByIdAsync(result.Response!.OrderId!.Value, default);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(newUser.Id, order.UserId);
            Assert.NotEmpty(order.OrderItems);
        }
    }
}
