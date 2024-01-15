using ecommerce.API.Models.UserController;
using ecommerce.Application.Features.Commands.SignUp;
using ecommerce.Application.Models.ValueObjects;
using ecommerce.ApplicationUnitTest.Utilities;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.API.UnitTest.Mappings.UserController
{
    public class SignUpMappingTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _autoMapperFixture;

        public SignUpMappingTest(AutoMapperFixture autoMapperFixture)
        {
            _autoMapperFixture = autoMapperFixture;
        }

        [Fact]
        public void MapFromModelToRequest_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new SignUpModel()
            {
                Email = UserTestUtility.ValidEmail,
                Password = PasswordTestUtility.ValidPassword,
                Name = UserTestUtility.ValidName,
                PhoneNumber = UserTestUtility.ValidPhoneNumber,
                Address = new AddressModel()
                {
                    Street = AddressTestUtility.ValidStreet,
                    ZipCode = AddressTestUtility.ValidZipCode,
                    City = AddressTestUtility.ValidCity,
                    Country = AddressTestUtility.ValidCountry
                }
            };

            // Act
            var result = _autoMapperFixture.Mapper.Map<SignUpCommandRequest>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.Email, result.Email);
            Assert.Equal(model.Password, result.Password);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.PhoneNumber, result.PhoneNumber);
            Assert.Equal(model.Address.Street, result.Address!.Street);
            Assert.Equal(model.Address.ZipCode, result.Address.ZipCode);
            Assert.Equal(model.Address.City, result.Address.City);
            Assert.Equal(model.Address.Country, result.Address.Country);
        }
    }
}
