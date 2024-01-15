using ecommerce.Application.Models.ValueObjects;
using ecommerce.Application.Utilities.Constants;
using ecommerce.Domain.Common.ValueObjects;
using FluentValidation.Results;
using FluentValidation;

namespace ecommerce.Application.Validations.ValueObjectValidations
{
    public class AddressValidation : AbstractValidator<AddressModel>
    {
        protected override bool PreValidate(ValidationContext<AddressModel> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure(context.PropertyPath, ConstantsUtility.AddressValidation.AddressRequired));
                return false;
            }

            return true;
        }

        public AddressValidation()
        {
            RuleFor(a => a.Street)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(ConstantsUtility.AddressValidation.StreetRequired)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.AddressValidation.StreetRequired)
                .MaximumLength(Address.StreetMaxLength)
                    .WithMessage(ConstantsUtility.AddressValidation.StreetLength_Max);

            RuleFor(a => a.ZipCode)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(ConstantsUtility.AddressValidation.ZipCodeRequired)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.AddressValidation.ZipCodeRequired)
                .MaximumLength(Address.ZipCodeMaxLength)
                    .WithMessage(ConstantsUtility.AddressValidation.ZipCodeLength_Max);

            RuleFor(a => a.City)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(ConstantsUtility.AddressValidation.CityRequired)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.AddressValidation.CityRequired)
                .MaximumLength(Address.CityMaxLength)
                    .WithMessage(ConstantsUtility.AddressValidation.CityLength_Max);

            RuleFor(a => a.Country)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(ConstantsUtility.AddressValidation.CountryRequired)
                .NotEmpty()
                    .WithMessage(ConstantsUtility.AddressValidation.CountryRequired)
                .MaximumLength(Address.CountryMaxLength)
                    .WithMessage(ConstantsUtility.AddressValidation.CountryLength_Max);
        }
    }
}
