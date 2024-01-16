using ecommerce.Application.Features.Commands.CloseTicket;
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

namespace ecommerce.ApplicationUnitTest.Features.Commands.CloseTicket
{
    public class CloseTicketCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private CloseTicketCommandHandler _closeTicketCommandHandler;

        public CloseTicketCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _closeTicketCommandHandler = new CloseTicketCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task CloseTicketCommandHandler_WhenValuesAreValid_ShouldCloseTicket()
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
            newOrder.AddMessageToTicket(TicketMessageTestUtility.ValidTicketMessage);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new CloseTicketCommandRequest()
            {
                OrderId = newOrder.Id
            };

            // Act
            var result = await _closeTicketCommandHandler.Handle(request, default);
            var order = await _unitofWorkFixture.UnitofWork.OrderRepository.GetByIdAsync(newOrder.Id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(TicketStatus.Closed, order!.TicketStatus);
        }

        [Fact]
        public async Task CloseTicketCommandHandler_WhenOrderDoesNotExist_ShouldReturnFalseAndReturnError()
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
            newOrder.AddMessageToTicket(TicketMessageTestUtility.ValidTicketMessage);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new CloseTicketCommandRequest()
            {
                OrderId = Guid.NewGuid()
            };

            // Act
            var result = await _closeTicketCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task CloseTicketCommandHandler_WhenTicketIsAlreadyClosed_ShouldReturnTrue()
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
            newOrder.AddMessageToTicket(TicketMessageTestUtility.ValidTicketMessage);
            newOrder.CloseTicket();

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.ProductRepository.AddAsync(newProduct);
            await _unitofWorkFixture.UnitofWork.OrderRepository.AddAsync(newOrder);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new CloseTicketCommandRequest()
            {
                OrderId = newOrder.Id
            };

            // Act
            var result = await _closeTicketCommandHandler.Handle(request, default);
            var order = await _unitofWorkFixture.UnitofWork.OrderRepository.GetByIdAsync(newOrder.Id);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
