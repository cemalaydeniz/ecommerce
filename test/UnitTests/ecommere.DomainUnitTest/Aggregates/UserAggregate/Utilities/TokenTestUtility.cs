namespace ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities
{
    public static class TokenTestUtility
    {
        public static string ValidTokenValue => "ValidTokenValue";
        public static DateTime ValidExpiresAt => DateTime.UtcNow.AddDays(1);

        #region Valid Values
        public static IEnumerable<object?[]> ValidExpiresAtValues()
        {
            yield return new object?[] { ValidExpiresAt };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidExpiresAtValues))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidExpiresAtValues()
        {
            yield return new object?[] { DateTime.UtcNow };
            yield return new object?[] { DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)) };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidExpiresAtValues))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> expiresAtValues)
        {
            foreach (var expiresAt in expiresAtValues())
            {
                yield return new object?[]
                {
                    ValidTokenValue,
                    expiresAt[0]
                };
            }
        }
    }
}
