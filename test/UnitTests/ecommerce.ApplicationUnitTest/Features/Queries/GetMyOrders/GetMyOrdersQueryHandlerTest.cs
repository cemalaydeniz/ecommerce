using ecommerce.Application.Features.Queries.GetMyOrders;
using ecommerce.Domain.Aggregates.OrderRepository;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Queries.GetMyOrders
{
    public class GetMyOrdersQueryHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private GetMyOrdersQueryHandler _getMyOrdersQueryHandler;

        public GetMyOrdersQueryHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _getMyOrdersQueryHandler = new GetMyOrdersQueryHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async void GetOrdersQueryHandler_WhenValuesAreValid_ShouldReturnOrders()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Order newOrder = new Order(newUser.Id,
                newUser.Name!,
                newUser.Addresses[0].Address,
                OrderTestUtility.ValidOrderItems);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new GetMyOrdersQueryRequest()
            {
                UserId = newUser.Id,
                Page = 1
            };

            // Act
            var result = await _getMyOrdersQueryHandler.Handle(request, default);

            // Assert
            Assert.NotEmpty(result.Orders);
            Assert.Equal(newOrder, result.Orders[0]);
        }
    }
}
