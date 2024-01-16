using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.AddTicketMessage
{
    public class AddTicketMessageCommandRequest : IRequest<ValidationBehaviorResult<AddTicketMessageCommandResponse>>
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string Content { get; set; } = null!;
    }
}
