using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.RefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<ValidationBehaviorResult<RefreshTokenCommandResponse>>
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
