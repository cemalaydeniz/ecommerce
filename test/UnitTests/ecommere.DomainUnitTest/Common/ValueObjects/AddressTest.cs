using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.DomainUnitTest.Common.ValueObjects
{
    public partial class AddressTest
    {
        [Theory]
        [MemberData(nameof(GenerateValidAddressValues))]
        public void CreateAddressValueObject_WhenValuesAreValid_ShouldThrowNoException(string street,
            string zipCode,
            string city,
            string country)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Address(street, zipCode, city, country);
            });

            // Assert
            Assert.Null(result);
        }

        public static IEnumerable<object?[]> GenerateValidAddressValues()
        {
            foreach (var values in AddressTestUtility.ValidValues())
            {
                yield return values;
            }
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidAddressValues))]
        public void CreateAddressValueObject_WhenValuesAreInvalid_ShouldThrowException(string street,
            string zipCode,
            string city,
            string country)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Address(street, zipCode, city, country);
            });

            // Assert
            Assert.NotNull(result);
        }

        public static IEnumerable<object?[]> GenerateInvalidAddressValues()
        {
            foreach (var values in AddressTestUtility.InvalidValues())
            {
                yield return values;
            }
        }
    }
}
