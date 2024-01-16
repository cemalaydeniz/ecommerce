using ecommerce.Application.Validations.OrderValidations;
using FluentValidation;

namespace ecommerce.Application.Features.Commands.AddTicketMessage
{
    public class AddTicketMessageCommandValidation : AbstractValidator<AddTicketMessageCommandRequest>
    {
        public AddTicketMessageCommandValidation()
        {
            RuleFor(x => x.Content)
                .SetValidator(new OrderMessageContentValidation());
        }
    }
}
