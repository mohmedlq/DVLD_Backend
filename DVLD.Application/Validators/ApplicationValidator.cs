using DVLD.Application.DTOs.Applications;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateApplicationValidator : AbstractValidator<CreateApplicationDto>
    {
        public CreateApplicationValidator()
        {
            RuleFor(x => x.ApplicantPersonID)
                .GreaterThan(0).WithMessage("Applicant Person ID must be greater than 0.");

            RuleFor(x => x.ApplicationTypeID)
                .GreaterThan(0).WithMessage("Application Type ID must be greater than 0.");

            RuleFor(x => x.PaidFees)
                .GreaterThanOrEqualTo(0).WithMessage("Paid fees cannot be negative.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }

    public class UpdateApplicationValidator : AbstractValidator<UpdateApplicationDto>
    {
        public UpdateApplicationValidator()
        {
            RuleFor(x => x.ApplicantPersonID)
                .GreaterThan(0).WithMessage("Applicant Person ID must be greater than 0.");

            RuleFor(x => x.ApplicationTypeID)
                .GreaterThan(0).WithMessage("Application Type ID must be greater than 0.");

            RuleFor(x => x.ApplicationStatus)
                .InclusiveBetween((byte)1, (byte)3).WithMessage("Application status must be 1 (New), 2 (Cancelled), or 3 (Completed).");

            RuleFor(x => x.PaidFees)
                .GreaterThanOrEqualTo(0).WithMessage("Paid fees cannot be negative.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }
}

