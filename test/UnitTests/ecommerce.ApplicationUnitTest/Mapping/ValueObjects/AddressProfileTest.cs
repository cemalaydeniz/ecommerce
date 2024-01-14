using ecommerce.Application.Models.ValueObjects;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.TestUtility.Fixtures;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.ApplicationUnitTest.Mapping.ValueObjects
{
    public class AddressProfileTest : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture _mapperFixture;

        public AddressProfileTest(AutoMapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
        }

        [Fact]
        public void MapFromAddressModelToAddress_WhenValuesAreValid_ShouldMapAll()
        {
            // Arrange
            var model = new AddressModel()
            {
                Street = AddressTestUtility.ValidStreet,
                ZipCode = AddressTestUtility.ValidZipCode,
                City = AddressTestUtility.ValidCity,
                Country = AddressTestUtility.ValidCountry
            };

            // Act
            var result = _mapperFixture.Mapper.Map<Address>(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.Street, result.Street);
            Assert.Equal(model.ZipCode, result.ZipCode);
            Assert.Equal(model.City, result.City);
            Assert.Equal(model.Country, result.Country);
        }
    }
}
