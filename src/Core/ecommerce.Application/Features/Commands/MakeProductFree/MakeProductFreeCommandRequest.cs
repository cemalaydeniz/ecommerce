using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.MakeProductFree
{
    public class MakeProductFreeCommandRequest : IRequest<ValidationBehaviorResult<MakeProductFreeCommandResponse>>
    {
        public Guid ProductId { get; set; }
    }
}
