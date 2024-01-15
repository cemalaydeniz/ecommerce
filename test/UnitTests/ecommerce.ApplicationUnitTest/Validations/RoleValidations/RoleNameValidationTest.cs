using ecommerce.Application.Validations.RoleValidations;
using ecommerce.DomainUnitTest.Aggregates.RoleAggregate.Utilities;

namespace ecommerce.ApplicationUnitTest.Validations.RoleValidations
{
    public class RoleNameValidationTest
    {
        private RoleNameValidation _validation;
        public RoleNameValidationTest()
        {
            _validation = new RoleNameValidation();
        }

        [Theory]
        [MemberData(nameof(RoleTestUtility.ValidNames), MemberType = typeof(RoleTestUtility))]
        public void ValidateRoleNameValue_WhenValueIsValid_ShouldReturnNoError(string roleName)
        {
            // Pre-condition
            if (string.IsNullOrWhiteSpace(roleName))
                return;

            // Act
            var result = _validation.Validate(roleName);

            // Assert
            Assert.Empty(result.Errors);
        }

        [Theory]
        [MemberData(nameof(RoleTestUtility.InvalidNames), MemberType = typeof(RoleTestUtility))]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateRoleNameValue_WhenValueIsInvalid_ShouldReturnError(string roleName)
        {
            // Act
            var result = _validation.Validate(roleName);

            // Assert
            Assert.NotEmpty(result.Errors);
        }
    }
}
