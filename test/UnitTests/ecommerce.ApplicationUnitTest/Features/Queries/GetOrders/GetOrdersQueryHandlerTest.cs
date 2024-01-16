using ecommerce.Application.Features.Queries.GetOrders;
using ecommerce.Domain.Aggregates.OrderAggregate;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Queries.GetOrders
{
    public class GetOrdersQueryHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private GetOrdersQueryHandler _getOrdersQueryHandler;

        public GetOrdersQueryHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _getOrdersQueryHandler = new GetOrdersQueryHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async void GetOrdersQueryHandler_WhenValuesAreValid_ShouldReturnOrders()
        {
            // Arrange
            Order newOrder = OrderTestUtility.ValidOrder;

            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new GetOrdersQueryRequest()
            {
                Page = 1,
                PageSize = 10
            };

            // Act
            var result = await _getOrdersQueryHandler.Handle(request, default);

            // Assert
            Assert.NotEmpty(result.Orders);
            Assert.Equal(newOrder, result.Orders[0]);
        }
    }
}
