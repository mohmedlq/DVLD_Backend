using DVLD.Application.DTOs.Licenses;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateLicenseValidator : AbstractValidator<CreateLicenseDto>
    {
        public CreateLicenseValidator()
        {
            RuleFor(x => x.ApplicationID)
                .GreaterThan(0).WithMessage("Application ID must be greater than 0.");

            RuleFor(x => x.DriverID)
                .GreaterThan(0).WithMessage("Driver ID must be greater than 0.");

            RuleFor(x => x.LicenseClass)
                .GreaterThan(0).WithMessage("License Class ID must be greater than 0.");

            RuleFor(x => x.PaidFees)
                .GreaterThanOrEqualTo(0).WithMessage("Paid fees cannot be negative.");

            RuleFor(x => x.IssueReason)
                .InclusiveBetween((byte)1, (byte)4).WithMessage("Issue reason must be between 1 and 4.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.");
        }
    }

    public class UpdateLicenseValidator : AbstractValidator<UpdateLicenseDto>
    {
        public UpdateLicenseValidator()
        {
            RuleFor(x => x.PaidFees)
                .GreaterThanOrEqualTo(0).WithMessage("Paid fees cannot be negative.");

            RuleFor(x => x.IssueReason)
                .InclusiveBetween((byte)1, (byte)4).WithMessage("Issue reason must be between 1 and 4.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.");
        }
    }
}

