namespace ecommerce.API.Models.UserController
{
    public class UpdateCredentialsModel
    {
        public string CurrentPassword { get; set; } = null!;
        public string? NewEmail { get; set; }
        public string? NewPassword { get; set; }
    }
}
