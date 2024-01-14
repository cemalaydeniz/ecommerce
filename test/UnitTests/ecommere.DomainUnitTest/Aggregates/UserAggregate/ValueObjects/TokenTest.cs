using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommere.DomainUnitTest.Aggregates.UserAggregate.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.UserAggregate.ValueObjects
{
    public class TokenTest
    {
        [Theory]
        [MemberData(nameof(TokenTestUtility.ValidValues), MemberType = typeof(TokenTestUtility))]
        public void CreateTokenValueObject_WhenValuesAreValid_ShouldThrowNoException(string value, DateTime expiresAt)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Token(value, expiresAt);
            });

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(TokenTestUtility.InvalidValues), MemberType = typeof(TokenTestUtility))]
        public void CreateTokenValueObject_WhenValuesAreInvalid_ShouldThrowException(string value, DateTime expiresAt)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Token(value, expiresAt);
            });

            // Assert
            Assert.NotNull(result);
        }
    }
}
