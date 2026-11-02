using DVLD.Application.DTOs.DetainedLicenses;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateDetainedLicenseValidator : AbstractValidator<CreateDetainedLicenseDto>
    {
        public CreateDetainedLicenseValidator()
        {
            RuleFor(x => x.LicenseID)
                .GreaterThan(0).WithMessage("License ID must be greater than 0.");

            RuleFor(x => x.FineFees)
                .GreaterThanOrEqualTo(0).WithMessage("Fine fees cannot be negative.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }

    public class ReleaseDetainedLicenseValidator : AbstractValidator<ReleaseDetainedLicenseDto>
    {
        public ReleaseDetainedLicenseValidator()
        {
            RuleFor(x => x.ReleasedByUserID)
                .GreaterThan(0).WithMessage("Released By User ID must be greater than 0.");

            RuleFor(x => x.ReleaseApplicationID)
                .GreaterThan(0).WithMessage("Release Application ID must be greater than 0.");
        }
    }

    public class UpdateDetainedLicenseValidator : AbstractValidator<UpdateDetainedLicenseDto>
    {
        public UpdateDetainedLicenseValidator()
        {
            RuleFor(x => x.FineFees)
                .GreaterThanOrEqualTo(0).WithMessage("Fine fees cannot be negative.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }
}

