namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class RoleValidation
        {
            public static readonly string NameRequired = "Role name is required";

            public static readonly string NameLength_MinMax = $"Name must be between {Domain.Aggregates.RoleAggregate.Role.NameMinLength} and {Domain.Aggregates.RoleAggregate.Role.NameMaxLength} characters";
        }
    }
}
