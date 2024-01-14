using ecommerce.Application.Authentication;
using ecommerce.Domain.Aggregates.UserAggregate;

namespace ecommerce.Application.Services
{
    public interface IJwtTokenService
    {
        JwtToken GenerateToken(User user, bool newRefreshToken = false);
    }
}
