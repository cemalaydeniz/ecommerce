using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.DomainUnitTest.Common.ValueObjects
{
    public partial class AddressTest
    {
        public class ComparisonTest
        {
            [Fact]
            public void CompareTwoAddressValueObjects_WhenBothAreSame_ShouldReturnTrue()
            {
                // Arrange
                Address firstAddress = AddressTestUtility.ValidAddress;
                Address secondAddress = AddressTestUtility.ValidAddress;

                // Act
                var result = firstAddress == secondAddress;

                // Assert
                Assert.True(result);
            }
        }
    }
}
