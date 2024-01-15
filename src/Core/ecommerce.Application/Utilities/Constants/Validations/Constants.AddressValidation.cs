namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class AddressValidation
        {
            public static readonly string AddressTitleRequired = "Address title is required";
            public static readonly string AddressRequired = "Address is required";
            public static readonly string StreetRequired = "Street is required";
            public static readonly string ZipCodeRequired = "Zip code is required";
            public static readonly string CityRequired = "City is required";
            public static readonly string CountryRequired = "Country is required";

            public static readonly string AddressTitleLength_Max = $"Address title cannot be longer than {Domain.Aggregates.UserAggregate.ValueObjects.UserAddress.TitleMaxLength} characters";
            public static readonly string StreetLength_Max = $"Street cannot be longer than {Domain.Common.ValueObjects.Address.StreetMaxLength} characters";
            public static readonly string ZipCodeLength_Max = $"Street cannot be longer than {Domain.Common.ValueObjects.Address.ZipCodeMaxLength} characters";
            public static readonly string CityLength_Max = $"Street cannot be longer than {Domain.Common.ValueObjects.Address.CityMaxLength} characters";
            public static readonly string CountryLength_Max = $"Street cannot be longer than {Domain.Common.ValueObjects.Address.CountryMaxLength} characters";
        }
    }
}
