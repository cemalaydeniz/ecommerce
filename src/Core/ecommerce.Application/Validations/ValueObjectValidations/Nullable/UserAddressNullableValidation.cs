﻿using ecommerce.Application.Models.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace ecommerce.Application.Validations.ValueObjectValidations.Nullable
{
    public class UserAddressNullableValidation : UserAddressValidation
    {
        protected override bool PreValidate(ValidationContext<UserAddressModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
                return false;

            return true;
        }

        public UserAddressNullableValidation() : base() { }
    }
}
