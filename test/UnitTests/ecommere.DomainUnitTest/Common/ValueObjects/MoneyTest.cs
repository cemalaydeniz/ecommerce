using ecommerce.Domain.Common.ValueObjects;
using ecommere.DomainUnitTest.Common.Utilities;

namespace ecommere.DomainUnitTest.Common.ValueObjects
{
    public partial class MoneyTest
    {
        [Theory]
        [MemberData(nameof(GenerateValidMoneyValues))]
        public void CreateMoneyValueObject_WhenValuesAreValid_ShouldThrowNoException(string currencyCode, decimal amount)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Money(currencyCode, amount);
            });

            // Assert
            Assert.Null(result);
        }

        public static IEnumerable<object?[]> GenerateValidMoneyValues()
        {
            foreach (var values in MoneyTestUtility.ValidValues())
            {
                yield return values;
            }
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidMoneyValues))]
        public void CreateMoneyValueObject_WhenValuesAreInvalid_ShouldThrowException(string currencyCode, decimal amount)
        {
            // Act
            var result = Record.Exception(() =>
            {
                new Money(currencyCode, amount);
            });

            // Assert
            Assert.NotNull(result);
        }

        public static IEnumerable<object?[]> GenerateInvalidMoneyValues()
        {
            foreach (var values in MoneyTestUtility.InvalidValues())
            {
                yield return values;
            }
        }
    }
}
