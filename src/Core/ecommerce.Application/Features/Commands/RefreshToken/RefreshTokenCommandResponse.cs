using ecommerce.Application.Authentication;

namespace ecommerce.Application.Features.Commands.RefreshToken
{
    public class RefreshTokenCommandResponse
    {
        public JwtToken Token { get; set; } = null!;
    }
}
