using ecommerce.Domain.Aggregates.RoleAggregate;
using ecommerce.TestUtility;

namespace ecommere.DomainUnitTest.Aggregates.RoleAggregate.Utilities
{
    public static class RoleTestUtility
    {
        public static string ValidName => "Name";

        public static Role ValidRole => new Role(ValidName);

        #region Valid Values
        public static IEnumerable<object?[]> ValidNames()
        {
            yield return new object?[] { ValidName };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidNames))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidNames()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Role.NameMinLength - 1) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(Role.NameMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidNames))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> names)
        {
            foreach (var name in names())
            {
                yield return new object?[]
                {
                    name[0]
                };
            }
        }
    }
}
