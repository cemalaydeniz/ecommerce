using ecommerce.API.Dtos.OrderController;
using ecommerce.Application.Features.Queries.GetMyOrders;
using ecommerce.Domain.Aggregates.OrderRepository;
using ecommerce.Domain.Aggregates.OrderRepository.Entities;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.OrderController
{
    public class GetMyOrdersMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public GetMyOrdersMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromResponseToDto_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var response = new GetMyOrdersQueryResponse()
            {
                Orders = new List<Order>()
                {
                    new Order(Guid.NewGuid(),
                        UserTestUtility.ValidName,
                        AddressTestUtility.ValidAddress,
                        new List<OrderItem>()
                        {
                            OrderItemTestUtility.ValidOrderItem
                        })
                }
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<GetMyOrdersDto>(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Orders[0].UserName, result.Orders[0].UserName);
            Assert.Equal(response.Orders[0].DeliveryAddress, result.Orders[0].DeliveryAddress);
            Assert.Equal(response.Orders[0].CreatedAt, result.Orders[0].CreatedAt);
            Assert.Equal(response.Orders[0].OrderItems.ElementAt(0).ProductName, result.Orders[0].Items[0].ProductName);
            Assert.Equal(response.Orders[0].OrderItems.ElementAt(0).UnitPrice, result.Orders[0].Items[0].UnitPrice);
            Assert.Equal(response.Orders[0].OrderItems.ElementAt(0).Quantity, result.Orders[0].Items[0].Quantity);
        }
    }
}
