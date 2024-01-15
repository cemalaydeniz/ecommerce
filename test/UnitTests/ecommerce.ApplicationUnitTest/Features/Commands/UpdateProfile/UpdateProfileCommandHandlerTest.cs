using ecommerce.Application.Features.Commands.UpdateProfile;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Features.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandlerTest : IClassFixture<UnitofWorkFixture>, IClassFixture<AutoMapperFixture>
    {
        private readonly UnitofWorkFixture _unitofWorkFixture;
        private readonly AutoMapperFixture _mapperFixture;

        private UpdateProfileCommandHandler _updateProfileCommandHandler;

        public UpdateProfileCommandHandlerTest(UnitofWorkFixture unitofWorkFixture,
            AutoMapperFixture mapperFixture)
        {
            _unitofWorkFixture = unitofWorkFixture;
            _mapperFixture = mapperFixture;

            _updateProfileCommandHandler = new UpdateProfileCommandHandler(_unitofWorkFixture.UnitofWork, _mapperFixture.Mapper);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenValuesAreValid_ShouldReturnTrueAndUpdateProfile()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateProfileCommandRequest()
            {
                UserId = newUser.Id,
                NewName = UserTestUtility.ValidName + "a",
                NewPhoneNumber = UserTestUtility.ValidPhoneNumber + "a",
                TitleofAddressToUpdate = UserAddressTestUtility.ValidTitle,
                UserAddress = new UserAddressModel()
                {
                    Title = UserAddressTestUtility.ValidTitle + "a",
                    Address = new AddressModel()
                    {
                        Street = AddressTestUtility.ValidStreet + "a",
                        ZipCode = AddressTestUtility.ValidZipCode + "a",
                        City = AddressTestUtility.ValidCity + "a",
                        Country = AddressTestUtility.ValidCountry + "a"
                    }
                }
            };

            // Act
            var result = await _updateProfileCommandHandler.Handle(request, default);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, false, false);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(request.NewName, user!.Name);
            Assert.Equal(request.NewPhoneNumber, user.PhoneNumber);
            Assert.Equal(request.UserAddress.Title, user.Addresses[0].Title);
            Assert.Equal(request.UserAddress.Address.Street, user.Addresses[0].Address.Street);
            Assert.Equal(request.UserAddress.Address.ZipCode, user.Addresses[0].Address.ZipCode);
            Assert.Equal(request.UserAddress.Address.City, user.Addresses[0].Address.City);
            Assert.Equal(request.UserAddress.Address.Country, user.Addresses[0].Address.Country);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenUserNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new UpdateProfileCommandRequest()
            {
                UserId = Guid.NewGuid()
            };

            // Act
            var result = await _updateProfileCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenAddressTitleIsNullButAddress_ShouldAddNewAddress()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateProfileCommandRequest()
            {
                UserId = newUser.Id,
                TitleofAddressToUpdate = null,
                UserAddress = new UserAddressModel()
                {
                    Title = UserAddressTestUtility.ValidTitle,
                    Address = new AddressModel()
                    {
                        Street = AddressTestUtility.ValidStreet,
                        ZipCode = AddressTestUtility.ValidZipCode,
                        City = AddressTestUtility.ValidCity,
                        Country = AddressTestUtility.ValidCountry
                    }
                }
            };

            // Act
            var result = await _updateProfileCommandHandler.Handle(request, default);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, false, false);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(request.UserAddress.Title, user!.Addresses[0].Title);
            Assert.Equal(request.UserAddress.Address.Street, user.Addresses[0].Address.Street);
            Assert.Equal(request.UserAddress.Address.ZipCode, user.Addresses[0].Address.ZipCode);
            Assert.Equal(request.UserAddress.Address.City, user.Addresses[0].Address.City);
            Assert.Equal(request.UserAddress.Address.Country, user.Addresses[0].Address.Country);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_WhenAddressIsNullButAddressTitle_ShouldRemoveAddress()
        {
            // Arrange
            User newUser = UserTestUtility.ValidUser;

            await _unitofWorkFixture.UnitofWork.UserRepository.AddAsync(newUser);
            await _unitofWorkFixture.UnitofWork.SaveChangesAsync();

            var request = new UpdateProfileCommandRequest()
            {
                UserId = newUser.Id,
                TitleofAddressToUpdate = UserAddressTestUtility.ValidTitle,
            };

            // Act
            var result = await _updateProfileCommandHandler.Handle(request, default);
            var user = await _unitofWorkFixture.UnitofWork.UserRepository.GetByIdAsync(newUser.Id, false, false);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(user!.Addresses);
        }

        [Fact]
        public async Task SignUpCommandHandler_WhenUserNotFound_ShouldReturnFalseAndReturnError()
        {
            // Arrange
            var request = new UpdateProfileCommandRequest()
            {
                UserId = Guid.NewGuid()
            };

            // Act
            var result = await _updateProfileCommandHandler.Handle(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors!);
        }
    }
}
