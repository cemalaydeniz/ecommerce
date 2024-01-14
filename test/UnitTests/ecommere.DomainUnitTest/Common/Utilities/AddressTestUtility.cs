using ecommerce.Domain.Common.ValueObjects;
using ecommerce.TestUtility;

namespace ecommerce.DomainUnitTest.Common.Utilities
{
    public static class AddressTestUtility
    {
        public static string ValidStreet => "street";
        public static string ValidZipCode => "zip code";
        public static string ValidCity => "city";
        public static string ValidCountry => "country";

        public static Address ValidAddress => new Address(ValidStreet,
            ValidZipCode,
            ValidCity,
            ValidCountry);

        #region Valid Values
        public static IEnumerable<object?[]> ValidStreets()
        {
            yield return new object?[] { ValidStreet };
        }

        public static IEnumerable<object?[]> ValidZipCodes()
        {
            yield return new object?[] { ValidZipCode };
        }

        public static IEnumerable<object?[]> ValidCities()
        {
            yield return new object?[] { ValidCity };
        }

        public static IEnumerable<object?[]> ValidCountries()
        {
            yield return new object?[] { ValidCountry };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidStreets,
                ValidZipCodes,
                ValidCities,
                ValidCountries))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidStreets()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Address.StreetMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidZipCodes()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Address.ZipCodeMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidCities()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Address.CityMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidCountries()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Address.CountryMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidStreets,
                InvalidZipCodes,
                InvalidCities,
                InvalidCountries))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> streets,
            Func<IEnumerable<object?[]>> zipCodes,
            Func<IEnumerable<object?[]>> cities,
            Func<IEnumerable<object?[]>> countries)
        {
            foreach (var street in streets())
            {
                yield return new object?[]
                {
                    street[0],
                    ValidZipCode,
                    ValidCity,
                    ValidCountry
                };
            }
            foreach (var zipCode in zipCodes())
            {
                yield return new object?[]
                {
                    ValidStreet,
                    zipCode[0],
                    ValidCity,
                    ValidCountry
                };
            }
            foreach (var city in cities())
            {
                yield return new object?[]
                {
                    ValidStreet,
                    ValidZipCode,
                    city[0],
                    ValidCountry
                };
            }
            foreach (var country in countries())
            {
                yield return new object?[]
                {
                    ValidCountry,
                    ValidZipCode,
                    ValidCity,
                    country[0]
                };
            }
        }
    }
}
