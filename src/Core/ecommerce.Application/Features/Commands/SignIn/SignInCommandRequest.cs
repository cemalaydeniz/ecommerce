using ecommerce.Application.Validations.Behaviors;
using MediatR;

namespace ecommerce.Application.Features.Commands.SignIn
{
    public class SignInCommandRequest : IRequest<ValidationBehaviorResult<SignInCommandResponse>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
