using DVLD.Application.DTOs.InternationalLicenses;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateInternationalLicenseValidator : AbstractValidator<CreateInternationalLicenseDto>
    {
        public CreateInternationalLicenseValidator()
        {
            RuleFor(x => x.ApplicationID)
                .GreaterThan(0).WithMessage("Application ID must be greater than 0.");

            RuleFor(x => x.DriverID)
                .GreaterThan(0).WithMessage("Driver ID must be greater than 0.");

            RuleFor(x => x.IssuedUsingLocalLicenseID)
                .GreaterThan(0).WithMessage("Local License ID must be greater than 0.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }

    public class UpdateInternationalLicenseValidator : AbstractValidator<UpdateInternationalLicenseDto>
    {
        public UpdateInternationalLicenseValidator()
        {
            RuleFor(x => x.ExpirationDate)
                .NotEmpty().WithMessage("Expiration date is required.");
        }
    }
}

