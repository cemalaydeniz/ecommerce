using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.CloseTicket
{
    public class CloseTicketCommandRequest : IRequest<ValidationBehaviorResult<CloseTicketCommandResponse>>
    {
        public Guid OrderId { get; set; }
    }
}
