using ecommerce.Application.Authentication;

namespace ecommerce.Application.Features.Commands.SignIn
{
    public class SignInCommandResponse
    {
        public JwtToken Token { get; set; } = null!;
    }
}
