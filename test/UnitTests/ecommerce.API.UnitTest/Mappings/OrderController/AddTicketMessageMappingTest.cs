using ecommerce.API.Models.OrderController;
using ecommerce.Application.Features.Commands.AddTicketMessage;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.OrderController
{
    public class AddTicketMessageMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public AddTicketMessageMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new AddTicketMessageModel()
            {
                Content = TicketMessageTestUtility.ValidContent
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<AddTicketMessageCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.Content, result.Content);
        }
    }
}
