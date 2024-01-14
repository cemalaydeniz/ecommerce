namespace ecommerce.Persistence.Authentication
{
    public static class JwtConstants
    {
        public const string Key_ConfigKey = "JwtSettings:Key";
        public const string Issuer_ConfigKey = "JwtSettings:Issuer";
        public const string Audience_ConfigKey = "JwtSettings:Audience";
        public const string AcessTokenLifespanInMinutes_ConfigKey = "JwtSettings:AccessTokenLifespanInMinutes";
        public const string RefreshTokenLifespanInDays_ConfigKey = "JwtSettings:RefreshTokenLifespanInDays";
    }
}
