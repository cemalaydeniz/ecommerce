using ecommerce.API.Models.UserController;
using ecommerce.Application.Features.Commands.UpdateProfile;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.UserController
{
    public class UpdateProfileMapping : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public UpdateProfileMapping(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new UpdateProfileModel()
            {
                NewName = UserTestUtility.ValidName,
                NewPhoneNumber = UserTestUtility.ValidPhoneNumber,
                TitleofAddressToUpdate = UserAddressTestUtility.ValidTitle,
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
            var result = _autoMapperFixture.Mapper.Map<UpdateProfileCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.NewName, result.NewName);
            Assert.Equal(model.NewPhoneNumber, result.NewPhoneNumber);
            Assert.Equal(model.TitleofAddressToUpdate, result.TitleofAddressToUpdate);
            Assert.Equal(model.UserAddress.Title, result.UserAddress!.Title);
            Assert.Equal(model.UserAddress.Address.Street, result.UserAddress.Address.Street);
            Assert.Equal(model.UserAddress.Address.ZipCode, result.UserAddress.Address.ZipCode);
            Assert.Equal(model.UserAddress.Address.City, result.UserAddress.Address.City);
            Assert.Equal(model.UserAddress.Address.Country, result.UserAddress.Address.Country);
        }
    }
}
