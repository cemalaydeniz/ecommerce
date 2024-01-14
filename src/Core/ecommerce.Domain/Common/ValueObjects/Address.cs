#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Common.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        private Address() { }

        /// <summary>
        /// Creates new address information
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        public Address(string street,
            string zipCode,
            string city,
            string country)
        {
            ValidateStreet(street);
            ValidateZipCode(zipCode);
            ValidateCity(city);
            ValidateCountry(country);

            Street = street;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return ZipCode;
            yield return City;
            yield return Country;
        }

        #region Validations
        public static readonly int StreetMaxLength = 100;
        public static readonly int ZipCodeMaxLength = 10;
        public static readonly int CityMaxLength = 100;
        public static readonly int CountryMaxLength = 100;

        private void ValidateStreet(string? street)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentNullException(nameof(street));
            if (street.Length > StreetMaxLength)
                throw new CharLengthOutofRangeException(nameof(street), StreetMaxLength);
        }

        private void ValidateZipCode(string? zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentNullException(nameof(zipCode));
            if (zipCode.Length > ZipCodeMaxLength)
                throw new CharLengthOutofRangeException(nameof(zipCode), ZipCodeMaxLength);
        }

        private void ValidateCity(string? city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if (city.Length > CityMaxLength)
                throw new CharLengthOutofRangeException(nameof(city), CityMaxLength);
        }

        private void ValidateCountry(string? country)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentNullException(nameof(country));
            if (country.Length > CountryMaxLength)
                throw new CharLengthOutofRangeException(nameof(country), CountryMaxLength);
        }
        #endregion
    }
}
