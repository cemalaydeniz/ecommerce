﻿namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class Role
        {
            public const string DefaultRole = "User";

            public const string User = "User";
            public const string Admin = "Admin";

            public const string RoleExists = "The role already exists";
            public const string RoleNotFound = "The role was not found";
        }
    }
}
