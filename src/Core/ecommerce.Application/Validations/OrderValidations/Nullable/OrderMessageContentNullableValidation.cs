using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.OrderValidations.Nullable
{
    public class OrderMessageContentNullableValidation : OrderMessageContentValidation
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
                return false;

            return true;
        }

        public OrderMessageContentNullableValidation() : base() { }
    }
}
