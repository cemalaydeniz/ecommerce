using ecommerce.Application.Models.ValueObjects;

namespace ecommerce.API.Models.UserController
{
    public class UpdateProfileModel
    {
        public string? NewName { get; set; }
        public string? NewPhoneNumber { get; set; }
        public string? TitleofAddressToUpdate { get; set; }
        public UserAddressModel? UserAddress { get; set; }
    }
}
