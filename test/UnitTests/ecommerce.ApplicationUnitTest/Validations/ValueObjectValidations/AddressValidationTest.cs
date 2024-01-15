using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Validations.ValueObjectValidations;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.ValueObjectValidations
{
    public class AddressValidationTest
    {
        private AddressValidation _validation;
        public AddressValidationTest()
        {
            _validation = new AddressValidation();
        }

        [Theory]
        [MemberData(nameof(AddressTestUtility.ValidValues), MemberType = typeof(AddressTestUtility))]
        public void ValidateAddressValues_WhenValuesAreValid_ShouldReturnNoError(string street,
            string zipCode,
            string city,
            string country)
        {
            // Arrange
            var address = new AddressModel()
            {
                Street = street,
                ZipCode = zipCode,
                City = city,
                Country = country
            };

            // Act
            var result = _validation.Validate(address);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(AddressTestUtility.InvalidValues), MemberType = typeof(AddressTestUtility))]
        public void ValidateAddressValues_WhenValuesAreInvalid_ShouldReturnError(string street,
            string zipCode,
            string city,
            string country)
        {
            // Arrange
            var address = new AddressModel()
            {
                Street = street,
                ZipCode = zipCode,
                City = city,
                Country = country
            };

            // Act
            var result = _validation.Validate(address);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
