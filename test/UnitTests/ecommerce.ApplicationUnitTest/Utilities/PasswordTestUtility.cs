namespace ecommerce.ApplicationUnitTest.Utilities
{
    public class PasswordTestUtility
    {
        public static string ValidPassword => "aaa111";

        public static IEnumerable<object?[]> ValidPasswords()
        {
            yield return new object?[] { ValidPassword };
        }

        public static IEnumerable<object?[]> InvalidPasswords()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { "aaaaaa" };
            yield return new object?[] { "111111" };
            yield return new object?[] { "aaaa1" };
        }
    }
}
