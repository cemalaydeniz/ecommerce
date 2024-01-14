namespace ecommerce.Application.Models.ValueObjects
{
    public class UserAddressModel
    {
        public string Title { get; set; } = null!;
        public AddressModel Address { get; set; } = null!;
    }
}
