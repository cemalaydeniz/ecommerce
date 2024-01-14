using ecommerce.Application.Authentication;
using ecommerce.Application.Services;
using ecommerce.Domain.Aggregates.UserAggregate;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ecommerce.Persistence.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtToken GenerateToken(User user, bool newRefreshToken = false)
        {
            var jwtSecretKey = _configuration[JwtConstants.Key_ConfigKey];
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey!));

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var issuer = _configuration[JwtConstants.Issuer_ConfigKey];
            var audience = _configuration[JwtConstants.Audience_ConfigKey];
            var accessTokenLifeSpan = _configuration[JwtConstants.AcessTokenLifespanInMinutes_ConfigKey];
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(accessTokenLifeSpan!)),
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256),
                claims: claims);

            JwtToken token = new JwtToken()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };

            if (newRefreshToken)
            {
                var refreshTokenLifeSpan = _configuration[JwtConstants.RefreshTokenLifespanInDays_ConfigKey];
                token.RefreshToken = GenerateRefreshToken();
                token.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(int.Parse(refreshTokenLifeSpan!));
            }

            return token;
        }

        private string GenerateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
