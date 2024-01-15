using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.API.Dtos.UserController
{
    public class GetUserProfileDto
    {
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public List<UserAddress> Addresses { get; set; } = null!;
    }
}
