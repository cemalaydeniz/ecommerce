using ecommerce.Persistence.Authentication;
using Microsoft.Extensions.Configuration;

namespace ecommerce.TestUtility.Fixtures
{
    public class JwtTokenServiceFixture : IDisposable
    {
        public JwtTokenService JwtTokenService { get; private set; }

        public JwtTokenServiceFixture()
        {
            JwtTokenService = new JwtTokenService(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { JwtConstants.Key_ConfigKey, StringGenerator.GenerateRandomStringStrictLength(20) },
                    { JwtConstants.Issuer_ConfigKey, "Issuer" },
                    { JwtConstants.Audience_ConfigKey, "Audience" },
                    { JwtConstants.AcessTokenLifespanInMinutes_ConfigKey, "15" },
                    { JwtConstants.RefreshTokenLifespanInDays_ConfigKey, "7" }
                }!).Build());
        }

        public void Dispose() { }
    }
}
