using ecommerce.API.Dtos.PaymentController;
using ecommerce.API.Models.PaymentController;
using ecommerce.Application.Features.Queries.InitiatePayment;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;
using Stripe;

namespace ecommerce.API.UnitTest.Mappings.PaymentController
{
    public class InitiatePaymentMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public InitiatePaymentMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new InitiatePaymentModel()
            {
                CurrencyCode = MoneyTestUtility.ValidCurrencyCode,
                Items = new List<InitiatePaymentModel.Item>()
                {
                    new InitiatePaymentModel.Item()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = OrderItemTestUtility.ValidQuantity
                    }
                }
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<InitiatePaymentQueryRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.CurrencyCode, result.CurrencyCode);
            Assert.Equal(model.Items[0].ProductId, result.Items[0].ProductId);
            Assert.Equal(model.Items[0].Quantity, result.Items[0].Quantity);
        }

        [Fact]
        public void MapFromResponseToDto_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var response = new InitiatePaymentQueryResponse()
            {
                PaymentIntent = new PaymentIntent()
                {
                    ClientSecret = Guid.NewGuid().ToString()
                }
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<InitiatePaymentDto>(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.PaymentIntent.ClientSecret, result.ClientSecret);
        }
    }
}
