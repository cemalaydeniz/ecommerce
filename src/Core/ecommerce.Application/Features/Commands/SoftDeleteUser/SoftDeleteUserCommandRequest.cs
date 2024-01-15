using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.SoftDeleteUser
{
    public class SoftDeleteUserCommandRequest : IRequest<ValidationBehaviorResult<SoftDeleteUserCommandResponse>>
    {
        public Guid UserId { get; set; }
    }
}
