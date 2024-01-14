using ecommerce.Domain.Common.ValueObjects;
using ecommere.DomainUnitTest.Common.Utilities;

namespace ecommere.DomainUnitTest.Common.ValueObjects
{
    public partial class MoneyTest
    {
        public class ComparisonTest
        {
            [Fact]
            public void CompareTwoMoneyObjectValues_WhenBothAreSame_ShouldReturnTrue()
            {
                // Arrange
                Money firstMoney = MoneyTestUtility.ValidMoney;
                Money secondMoney = MoneyTestUtility.ValidMoney;

                // Act
                var result = firstMoney == secondMoney;

                // Assert
                Assert.True(result);
            }
        }
    }
}
