using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Validations.ValueObjectValidations;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.ValueObjectValidations
{
    public class MoneyValidationTest
    {
        private MoneyValidation _validation;
        public MoneyValidationTest()
        {
            _validation = new MoneyValidation();
        }

        [Theory]
        [MemberData(nameof(MoneyTestUtility.ValidValues), MemberType = typeof(MoneyTestUtility))]
        public void ValidateMoneyValues_WhenValuesAreValid_ShouldReturnNoError(string currencyCode, decimal amount)
        {
            // Arrange
            var money = new MoneyModel()
            {
                CurrencyCode = currencyCode,
                Amount = amount
            };

            // Act
            var result = _validation.Validate(money);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(MoneyTestUtility.InvalidValues), MemberType = typeof(MoneyTestUtility))]
        public void ValidateMoneyValues_WhenValuesAreInvalid_ShouldReturnError(string currencyCode, decimal amount)
        {
            // Arrange
            var money = new MoneyModel()
            {
                CurrencyCode = currencyCode,
                Amount = amount
            };

            // Act
            var result = _validation.Validate(money);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
