namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class UserValidation
        {
            public static readonly string PasswordRegex = @"^(?=.*[A-Za-z])(?=.*\d)[^\s]+$";
            public static readonly int PasswordMinLength = 6;
            public static readonly int PasswordMaxLength = 100;

            public static readonly string PasswordRequired = "Password is required";
            public static readonly string PasswordLength_MinMax = $"Name must be between {PasswordMinLength} and {PasswordMaxLength} characters";
            public static readonly string InvalidPassword = "Password must have at least 1 character and 1 number";

            public static readonly string NameRequired = "Name is required";
            public static readonly string NameLength_MinMax = $"Name must be between {Domain.Aggregates.UserAggregate.User.NameMinLength} and {Domain.Aggregates.UserAggregate.User.NameMaxLength} characters";

            public static readonly string PhoneNumberRequired = "Phone number is required";
            public static readonly string PhoneNumberLength_Max = $"Phone number cannot be longer than {Domain.Aggregates.UserAggregate.User.PhoneNumberMaxLength} characters";

            public static readonly string EmailRequired = "Email is required";
            public static readonly string EmailLength_Max = $"Email cannot be longer than {Domain.Aggregates.UserAggregate.User.EmailMaxLength} characters";
            public static readonly string InvalidEmail = "Email is invalid";
        }
    }
}
