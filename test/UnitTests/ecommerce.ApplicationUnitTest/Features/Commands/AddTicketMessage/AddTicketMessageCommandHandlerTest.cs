using ecommerce.Application.Features.Commands.AddTicketMessage;
using ecommerce.Domain.Aggregates.OrderAggregate;
using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using ecommerce.Domain.Aggregates.OrderAggregate.Enums;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.ProductAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.AddTicketMessage
{
    public class AddTicketMessageCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private AddTicketMessageCommandHandler _addTicketMessageCommandHandler;

        public AddTicketMessageCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _addTicketMessageCommandHandler = new AddTicketMessageCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task AddTicketMessageCommandHandler_WhenValuesAreValid_ShouldAddNewMessageAndChangeStatus()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct = ProductTestUtility.ValidProduct;
            Order newOrder = new Order(newUser.Id, newUser.Name!, newUser.Addresses[0].Address, new List<OrderItem>()
                {
                    new OrderItem(newProduct.Id, newProduct.Name, newProduct.Prices[0], OrderItemTestUtility.ValidQuantity)
                });

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AddTicketMessageCommandRequest()
            {
                UserId = newUser.Id,
                OrderId = newOrder.Id,
                Content = TicketMessageTestUtility.ValidContent
            };

            // Act
            var result = await _addTicketMessageCommandHandler.Handle(request, default);
            var order = await _unitofWorkFixture.UnitofWork.OrderRepository.GetByIdAsync(newOrder.Id, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(TicketStatus.Opened, order!.TicketStatus);
            Assert.NotEmpty(order.TicketMessages);
        }

        [Fact]
        public async Task AddTicketMessageCommandHandler_WhenUserDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new AddTicketMessageCommandRequest()
            {
                UserId = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                Content = TicketMessageTestUtility.ValidContent
            };

            // Act
            var result = await _addTicketMessageCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task AddTicketMessageCommandHandler_WhenOrderDoesNotExist_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AddTicketMessageCommandRequest()
            {
                UserId = newUser.Id,
                OrderId = Guid.NewGuid(),
                Content = TicketMessageTestUtility.ValidContent
            };

            // Act
            var result = await _addTicketMessageCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task AddTicketMessageCommandHandler_WhenUserIsWrong_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct = ProductTestUtility.ValidProduct;
            Order newOrder = new Order(Guid.NewGuid(), newUser.Name!, newUser.Addresses[0].Address, new List<OrderItem>()
                {
                    new OrderItem(newProduct.Id, newProduct.Name, newProduct.Prices[0], OrderItemTestUtility.ValidQuantity)
                });

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AddTicketMessageCommandRequest()
            {
                UserId = newUser.Id,
                OrderId = newOrder.Id,
                Content = TicketMessageTestUtility.ValidContent
            };

            // Act
            var result = await _addTicketMessageCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task AddTicketMessageCommandHandler_WhenUserIsAdmin_ShouldAddNewMessage()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Role newRole = new Role(Application.Utilities.Constants.ConstantsUtility.Role.Admin);
            Product newProduct = ProductTestUtility.ValidProduct;
            Order newOrder = new Order(Guid.NewGuid(), newUser.Name!, newUser.Addresses[0].Address, new List<OrderItem>()
                {
                    new OrderItem(newProduct.Id, newProduct.Name, newProduct.Prices[0], OrderItemTestUtility.ValidQuantity)
                });
            newRole.AssignToUser(newUser);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AddTicketMessageCommandRequest()
            {
                UserId = newUser.Id,
                OrderId = newOrder.Id,
                Content = TicketMessageTestUtility.ValidContent
            };

            // Act
            var result = await _addTicketMessageCommandHandler.Handle(request, default);
            var order = await _unitofWorkFixture.UnitofWork.OrderRepository.GetByIdAsync(newOrder.Id, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(order!.TicketMessages);
        }

        [Fact]
        public async Task AddTicketMessageCommandHandler_WhenTicketIsClosed_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Product newProduct = ProductTestUtility.ValidProduct;
            Order newOrder = new Order(newUser.Id, newUser.Name!, newUser.Addresses[0].Address, new List<OrderItem>()
                {
                    new OrderItem(newProduct.Id, newProduct.Name, newProduct.Prices[0], OrderItemTestUtility.ValidQuantity)
                });
            newOrder.AddMessageToTicket(TicketMessageTestUtility.ValidTicketMessage);
            newOrder.CloseTicket();
            newOrder.ClearDomainEvents();

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new AddTicketMessageCommandRequest()
            {
                UserId = newUser.Id,
                OrderId = newOrder.Id,
                Content = TicketMessageTestUtility.ValidContent
            };

            // Act
            var result = await _addTicketMessageCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
