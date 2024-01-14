using ecommerce.Application.Models.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;
using ecommerce.TestUtility.Fixtures;

namespace ecommerce.ApplicationUnitTest.Mapping.ValueObjects
{
    public class UserAddressProfileTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _mapperFixture;

        public UserAddressProfileTest(AutoMapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
        }

        [Fact]
        public void MapFromAddressModelToAddress_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new UserAddressModel()
            {
                Title = UserAddressTestUtility.ValidTitle,
                Address = new AddressModel()
                {
                    Street = AddressTestUtility.ValidStreet,
                    ZipCode = AddressTestUtility.ValidZipCode,
                    City = AddressTestUtility.ValidCity,
                    Country = AddressTestUtility.ValidCountry
                }
            };

            // Act
            var result = _mapperFixture.Mapper.Map<UserAddress>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.Title, result.Title);
            Assert.Equal(model.Address.Street, result.Address.Street);
            Assert.Equal(model.Address.ZipCode, result.Address.ZipCode);
            Assert.Equal(model.Address.City, result.Address.City);
            Assert.Equal(model.Address.Country, result.Address.Country);
        }
    }
}
