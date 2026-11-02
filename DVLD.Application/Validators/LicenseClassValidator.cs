using DVLD.Application.DTOs.LicenseClasses;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateLicenseClassValidator : AbstractValidator<CreateLicenseClassDto>
    {
        public CreateLicenseClassValidator()
        {
            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("Class name is required.")
                .MinimumLength(3).WithMessage("Class name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Class name cannot exceed 50 characters.");

            RuleFor(x => x.ClassDescription)
                .NotEmpty().WithMessage("Class description is required.")
                .MaximumLength(500).WithMessage("Class description cannot exceed 500 characters.");

            RuleFor(x => x.MinimumAllowedAge)
                .InclusiveBetween((byte)16, (byte)100)
                .WithMessage("Minimum allowed age must be between 16 and 100.");

            RuleFor(x => x.DefaultValidityLength)
                .InclusiveBetween((byte)1, (byte)20)
                .WithMessage("Default validity length must be between 1 and 20 years.");

            RuleFor(x => x.ClassFees)
                .GreaterThanOrEqualTo(0).WithMessage("Class fees must be greater than or equal to 0.");
        }
    }

    public class UpdateLicenseClassValidator : AbstractValidator<UpdateLicenseClassDto>
    {
        public UpdateLicenseClassValidator()
        {
            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("Class name is required.")
                .MinimumLength(3).WithMessage("Class name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Class name cannot exceed 50 characters.");

            RuleFor(x => x.ClassDescription)
                .NotEmpty().WithMessage("Class description is required.")
                .MaximumLength(500).WithMessage("Class description cannot exceed 500 characters.");

            RuleFor(x => x.MinimumAllowedAge)
                .InclusiveBetween((byte)16, (byte)100)
                .WithMessage("Minimum allowed age must be between 16 and 100.");

            RuleFor(x => x.DefaultValidityLength)
                .InclusiveBetween((byte)1, (byte)20)
                .WithMessage("Default validity length must be between 1 and 20 years.");

            RuleFor(x => x.ClassFees)
                .GreaterThanOrEqualTo(0).WithMessage("Class fees must be greater than or equal to 0.");
        }
    }
}

