using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Aggregates.OrderAggregate.Entities;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.OrderValidations
{
    public class OrderMessageContentValidation : AbstractValidator<string>
    {
        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(context.InstanceToValidate))
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.OrderValidation.MessageContentRequired));
                return false;
            }

            return true;
        }

        public OrderMessageContentValidation()
        {
            RuleFor(c => c)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.OrderValidation.MessageContentRequired)
                .Length(TicketMessage.ContentMinLength, TicketMessage.ContentMaxLength)
                    .WithMessage(ConstantsUtility.OrderValidation.MessageContentLength_MinMax);
        }
    }
}
