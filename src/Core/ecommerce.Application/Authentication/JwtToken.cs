namespace ecommerce.Application.Authentication
{
    public class JwtToken
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}
