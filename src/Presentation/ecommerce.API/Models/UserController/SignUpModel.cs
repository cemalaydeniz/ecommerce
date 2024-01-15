using ecommerce.Application.Models.ValueObjects;

namespace ecommerce.API.Models.UserController
{
    public class SignUpModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public AddressModel? Address { get; set; }
    }
}
