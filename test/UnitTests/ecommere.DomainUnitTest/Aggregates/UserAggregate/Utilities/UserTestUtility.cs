using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.TestUtility;

namespace ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities
{
    public static class UserTestUtility
    {
        public static string ValidEmail => "test@example.com";
        public static string ValidPasswordHashed => BCrypt.Net.BCrypt.HashPassword("aaa111");
        public static string ValidName => "Name";
        public static string ValidPhoneNumber => "+123456789";

        public static User ValidUser => new User(ValidEmail,
            ValidPasswordHashed,
            ValidName,
            ValidPhoneNumber,
            UserAddressTestUtility.ValidUserAddress);

        #region Valid Values
        public static IEnumerable<object?[]> ValidEmails()
        {
            yield return new object?[] { ValidEmail };
        }

        public static IEnumerable<object?[]> ValidPasswordHasheds()
        {
            yield return new object?[] { ValidPasswordHashed };
        }

        public static IEnumerable<object?[]> ValidNames()
        {
            yield return new object?[] { ValidName };
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
        }

        public static IEnumerable<object?[]> ValidPhoneNumbers()
        {
            yield return new object?[] { ValidName };
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
        }

        public static IEnumerable<object?[]> ValidUserAddresses()
        {
            yield return new object?[] { UserAddressTestUtility.ValidUserAddress };
            yield return new object?[] { null };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidEmails,
                ValidPasswordHasheds,
                ValidNames,
                ValidPhoneNumbers,
                ValidUserAddresses))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidEmails()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(1) +
                "@" + StringGenerator.GenerateRandomStringStrictLength(1) + "." +
                StringGenerator.GenerateRandomStringStrictLength(User.EmailMaxLength) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(5) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(5) +
                "@" + StringGenerator.GenerateRandomStringStrictLength(5) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(5) +
                "@" + StringGenerator.GenerateRandomStringStrictLength(5) + "." };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(5) +
                "@" + StringGenerator.GenerateRandomStringStrictLength(5) + "." +
                StringGenerator.GenerateRandomStringStrictLength(5) + "." };
        }

        public static IEnumerable<object?[]> InvalidPasswordHasheds()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
        }

        public static IEnumerable<object?[]> InvalidNames()
        {
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(User.NameMinLength - 1) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(User.NameMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidPhoneNumbers()
        {
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(User.PhoneNumberMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidEmails,
                InvalidPasswordHasheds,
                InvalidNames,
                InvalidPhoneNumbers,
                null))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> emails,
            Func<IEnumerable<object?[]>> passwordHasheds,
            Func<IEnumerable<object?[]>> names,
            Func<IEnumerable<object?[]>> phoneNumbers,
            Func<IEnumerable<object?[]>>? userAddresses)
        {
            foreach (var email in emails())
            {
                yield return new object?[]
                {
                    email[0],
                    ValidPasswordHashed,
                    ValidName,
                    ValidPhoneNumber,
                    UserAddressTestUtility.ValidUserAddress
                };
            }
            foreach (var passwordHashed in passwordHasheds())
            {
                yield return new object?[]
                {
                    ValidEmail,
                    passwordHashed[0],
                    ValidName,
                    ValidPhoneNumber,
                    UserAddressTestUtility.ValidUserAddress
                };
            }
            foreach (var name in names())
            {
                yield return new object?[]
                {
                    ValidEmail,
                    ValidPasswordHashed,
                    name[0],
                    ValidPhoneNumber,
                    UserAddressTestUtility.ValidUserAddress
                };
            }
            foreach (var phoneNumber in phoneNumbers())
            {
                yield return new object?[]
                {
                    ValidEmail,
                    ValidPasswordHashed,
                    ValidName,
                    phoneNumber[0],
                    UserAddressTestUtility.ValidUserAddress
                };
            }
            if (userAddresses != null)
            {
                foreach (var userAddress in userAddresses())
                {
                    yield return new object?[]
                    {
                        ValidEmail,
                        ValidPasswordHashed,
                        ValidName,
                        ValidPhoneNumber,
                        userAddress[0]
                    };
                }
            }
        }
    }
}
