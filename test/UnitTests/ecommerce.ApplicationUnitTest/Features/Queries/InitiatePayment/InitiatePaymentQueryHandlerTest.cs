using ecommerce.Application.Features.Queries.InitiatePayment;
using ecommerce.Application.Services;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;
using Moq;

namespace ecommerce.ApplicationUnitTest.Features.Queries.InitiatePayment
{
    public class InitiatePaymentQueryHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly Mock<IStripeService> _mockStripeService;

        private InitiatePaymentQueryHandler _initiatePaymentQueryHandler;

        public InitiatePaymentQueryHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.Setup(m => m.CreatePaymentIntent(It.IsAny<User>(),
                It.IsAny<UserAddress>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>()))
                .Returns(new Stripe.PaymentIntent() { ClientSecret = "Client-Secret" });

            _initiatePaymentQueryHandler = new InitiatePaymentQueryHandler(unitofWorkFixture.UnitofWork, _mockStripeService.Object);
        }

        [Fact]
        public async Task InitiatePaymentQueryHandler_WhenValuesAreValid_ShouldReturnPaymentIntent()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct1 = ProductTestUtility.ValidProduct;
            Product newProduct2 = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new InitiatePaymentQueryRequest()
            {
                UserId = newUser.Id,
                AddressTitle = newUser.Addresses[0].Title,
                CurrencyCode = newProduct1.Prices[0].CurrencyCode,
                Items = new List<InitiatePaymentQueryRequest.Item>()
                {
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct1.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    },
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct2.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = await _initiatePaymentQueryHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Response);
            Assert.NotNull(result.Response.PaymentIntent);
        }

        [Fact]
        public async Task InitiatePaymentQueryHandler_WhenUserNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            Product newProduct1 = ProductTestUtility.ValidProduct;
            Product newProduct2 = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new InitiatePaymentQueryRequest()
            {
                UserId = Guid.NewGuid(),
                AddressTitle = UserAddressTestUtility.ValidTitle,
                CurrencyCode = newProduct1.Prices[0].CurrencyCode,
                Items = new List<InitiatePaymentQueryRequest.Item>()
                {
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct1.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    },
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct2.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = await _initiatePaymentQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors!);
        }

        [Fact]
        public async Task InitiatePaymentQueryHandler_WhenUserNameIsNull_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = new User(UserTestUtility.ValidEmail, UserTestUtility.ValidPasswordHashed,
                null,
                UserTestUtility.ValidPhoneNumber,
                UserAddressTestUtility.ValidUserAddress);
            Product newProduct1 = ProductTestUtility.ValidProduct;
            Product newProduct2 = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new InitiatePaymentQueryRequest()
            {
                UserId = newUser.Id,
                AddressTitle = newUser.Addresses[0].Title,
                CurrencyCode = newProduct1.Prices[0].CurrencyCode,
                Items = new List<InitiatePaymentQueryRequest.Item>()
                {
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct1.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    },
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct2.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = await _initiatePaymentQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors!);
        }

        [Fact]
        public async Task InitiatePaymentQueryHandler_WhenUserHasNoAddress_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = new User(UserTestUtility.ValidEmail, UserTestUtility.ValidPasswordHashed,
                UserTestUtility.ValidName,
                UserTestUtility.ValidPhoneNumber,
                null);
            Product newProduct1 = ProductTestUtility.ValidProduct;
            Product newProduct2 = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new InitiatePaymentQueryRequest()
            {
                UserId = newUser.Id,
                AddressTitle = UserAddressTestUtility.ValidTitle,
                CurrencyCode = newProduct1.Prices[0].CurrencyCode,
                Items = new List<InitiatePaymentQueryRequest.Item>()
                {
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct1.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    },
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct2.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = await _initiatePaymentQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors!);
        }

        [Fact]
        public async Task InitiatePaymentQueryHandler_WhenThereIsUngroupedProducts_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct1 = ProductTestUtility.ValidProduct;
            Product newProduct2 = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new InitiatePaymentQueryRequest()
            {
                UserId = newUser.Id,
                AddressTitle = newUser.Addresses[0].Title,
                CurrencyCode = newProduct1.Prices[0].CurrencyCode,
                Items = new List<InitiatePaymentQueryRequest.Item>()
                {
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct1.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    },
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct1.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = await _initiatePaymentQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors!);
        }

        [Fact]
        public async Task InitiatePaymentQueryHandler_WhenProductIsNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct1 = ProductTestUtility.ValidProduct;
            Product newProduct2 = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new InitiatePaymentQueryRequest()
            {
                UserId = newUser.Id,
                AddressTitle = newUser.Addresses[0].Title,
                CurrencyCode = newProduct1.Prices[0].CurrencyCode,
                Items = new List<InitiatePaymentQueryRequest.Item>()
                {
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = OrderItemTestUtility.ValidQuantity
                    },
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct2.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = await _initiatePaymentQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors!);
        }

        [Fact]
        public async Task InitiatePaymentQueryHandler_WhenProductIsSoftDeleted_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct1 = ProductTestUtility.ValidProduct;
            Product newProduct2 = ProductTestUtility.ValidProduct;
            newProduct1.Delete();

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new InitiatePaymentQueryRequest()
            {
                UserId = newUser.Id,
                AddressTitle = newUser.Addresses[0].Title,
                CurrencyCode = newProduct1.Prices[0].CurrencyCode,
                Items = new List<InitiatePaymentQueryRequest.Item>()
                {
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct1.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    },
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct2.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = await _initiatePaymentQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors!);
        }

        [Fact]
        public async Task InitiatePaymentQueryHandler_WhenCurrencyCodeIsWrong_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct1 = ProductTestUtility.ValidProduct;
            Product newProduct2 = ProductTestUtility.ValidProduct;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct1);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct2);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new InitiatePaymentQueryRequest()
            {
                UserId = newUser.Id,
                AddressTitle = newUser.Addresses[0].Title,
                CurrencyCode = "QQQ",
                Items = new List<InitiatePaymentQueryRequest.Item>()
                {
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct1.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    },
                    new InitiatePaymentQueryRequest.Item()
                    {
                        ProductId = newProduct2.Id,
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = await _initiatePaymentQueryHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors!);
        }
    }
}
