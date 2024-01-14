using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.UserAggregate.ValueObjects
{
    public class UserAddressTest
    {
        [Theory]
        [MemberData(nameof(UserAddressTestUtility.ValidValues), MemberType = typeof(UserAddressTestUtility))]
        public void CreateUserAddressValueObject_WhenValuesAreValid_ShouldThrowNoException(string title, Address address)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new UserAddress(title, address);
            });

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(UserAddressTestUtility.InvalidValues), MemberType = typeof(UserAddressTestUtility))]
        public void CreateUserAddressValueObject_WhenValuesAreInvalid_ShouldThrowException(string title, Address address)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new UserAddress(title, address);
            });

            // Assert
            Assert.NotNull(result);
        }
    }
}
