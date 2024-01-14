using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.TestUtility;
using ecommere.DomainUnitTest.Common.Utilities;

namespace ecommere.DomainUnitTest.Aggregates.UserAggregate.Utilities
{
    public static class UserAddressTestUtility
    {
        public static string ValidTitle => "Title";

        public static UserAddress ValidUserAddress => new UserAddress(ValidTitle, AddressTestUtility.ValidAddress);

        #region Valid Values
        public static IEnumerable<object?[]> ValidTitles()
        {
            yield return new object?[] { ValidTitle };
        }

        public static IEnumerable<object?[]> ValidAddresses()
        {
            yield return new object?[] { AddressTestUtility.ValidAddress };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidTitles, ValidAddresses))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidTitles()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(UserAddress.TitleMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidAddresses()
        {
            yield return new object?[] { null };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidTitles, InvalidAddresses))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> titles,
            Func<IEnumerable<object?[]>> addresses)
        {
            foreach (var title in titles())
            {
                yield return new object?[]
                {
                    title[0],
                    AddressTestUtility.ValidAddress
                };
            }
            foreach (var address in addresses())
            {
                yield return new object?[]
                {
                    ValidTitle,
                    address[0]
                };
            }
        }
    }
}
