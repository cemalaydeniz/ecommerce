using ecommerce.Application.Events.UserEvents.UserCreatedEvent;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.SeedWork;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.ApplicationUnitTest.Events.UserEvents.UserCreatedEvent
{
    public class AddDefaultRoleHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly Mock<ILogger<AddDefaultRoleHandler>> _mockLogger;

        private AddDefaultRoleHandler _addDefaultRoleToUserTest;

        public AddDefaultRoleHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _mockLogger = new Mock<ILogger<AddDefaultRoleHandler>>();

            _addDefaultRoleToUserTest = new AddDefaultRoleHandler(_unitofWorkFixture.UnitofWork, _mockLogger.Object);
        }

        [Fact]
        public async Task AddDefaultRoleToUser_WhenUserIsAddedToDb_ShouldAssignDefaultRoleToUser()
        {
            // Arrange
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(new Role(Application.Utilities.Constants.ConstantsUtility.Role.DefaultRole));
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            _unitofWorkFixture.MockMediator.Setup(m => m.Publish(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()))
                .Callback<IDomainEvent, CancellationToken>(async (notification, cancellationToken) =>
                {
                    if (notification is UserCreated userCreated)
                    {
                        await _addDefaultRoleToUserTest.Handle(userCreated, cancellationToken);
                    }
                });

            User newUser = UserTestUtility.ValidUser;
            Guid userId = newUser.Id;

            // Act
            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var createdUser = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(userId, true, false);
            var defaultRole = await _unitofWorkFixture.UnitofWork.RoleRepository.GetByNameAsync(Application.Utilities.Constants.ConstantsUtility.Role.DefaultRole, true);

            // Assert
            Assert.NotNull(createdUser);
            Assert.NotNull(defaultRole);
            Assert.Contains(Application.Utilities.Constants.ConstantsUtility.Role.DefaultRole, createdUser.Roles.Select(r => r.Name));
            Assert.Contains(userId, defaultRole.Users.Select(u => u.Id));
        }
    }
}
