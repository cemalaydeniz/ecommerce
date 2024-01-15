using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.PaymentCheck
{
    public class PaymentCheckCommandRequest : IRequest<ValidationBehaviorResult<PaymentCheckCommandResponse>>
    {
        public string Header { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}
