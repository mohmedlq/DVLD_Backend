using DVLD.Application.DTOs.DeletedLicenses;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateDeletedLicenseValidator : AbstractValidator<CreateDeletedLicenseDto>
    {
        public CreateDeletedLicenseValidator()
        {
            RuleFor(x => x.LicenseID)
                .GreaterThan(0).WithMessage("License ID must be greater than 0.");

            RuleFor(x => x.ApplicationID)
                .GreaterThan(0).WithMessage("Application ID must be greater than 0.");

            RuleFor(x => x.DriverID)
                .GreaterThan(0).WithMessage("Driver ID must be greater than 0.");

            RuleFor(x => x.LicenseClass)
                .GreaterThan(0).WithMessage("License Class ID must be greater than 0.");
        }
    }
}

