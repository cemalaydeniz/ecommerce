using ecommerce.Application.Features.Commands.SoftDeleteUser;
using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.SoftDeleteUser
{
    public class SoftDeleteUserCommandHandlerTest : IClassFixture<UnitofWorkFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;

        private SoftDeleteUserCommandHandler _softDeleteUserCommandHandler;

        public SoftDeleteUserCommandHandlerTest(UnitofWorkFixture unitofWorkFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;

            _softDeleteUserCommandHandler = new SoftDeleteUserCommandHandler(_unitofWorkFixture.UnitofWork);
        }

        [Fact]
        public async Task SoftDeleteUserCommandHandler_WhenValuesAreValid_ShouldSoftDeleteUserAndRemoveItsRoles()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            Role newRole = RoleTestUtility.ValidRole;
            newRole.AssignToUser(newUser);

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.RoleRepository.AddAsync(newRole);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SoftDeleteUserCommandRequest()
            {
                UserId = newUser.Id
            };

            // Act
            var result = await _softDeleteUserCommandHandler.Handle(request, default);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, false, true);
            var role = await _unitofWorkFixture.UnitofWork.RoleRepository.GetByIdAsync(newRole.Id, true);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(user!.IsDeleted);
            Assert.Null(user.Email);
            Assert.Null(user.PasswordHashed);
            Assert.Null(user.Name);
            Assert.Null(user.PhoneNumber);
            Assert.Empty(user.Addresses);
            Assert.False((bool)role!.Users.Any(u => u == newUser));
        }

        [Fact]
        public async Task SoftDeleteUserCommandHandler_WhenUserDoesNotExist_ShouldReturnFalseAndReturnErrorr()
        {
            // Arrange
            var request = new SoftDeleteUserCommandRequest()
            {
                UserId = Guid.NewGuid()
            };

            // Act
            var result = await _softDeleteUserCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task SoftDeleteUserCommandHandler_WhenUserIsAlreadySoftDeleted_ShouldReturnTrue()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;
            newUser.Delete();

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new SoftDeleteUserCommandRequest()
            {
                UserId = newUser.Id
            };

            // Act
            var result = await _softDeleteUserCommandHandler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
