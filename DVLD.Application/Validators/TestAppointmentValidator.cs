using DVLD.Application.DTOs.TestAppointments;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateTestAppointmentValidator : AbstractValidator<CreateTestAppointmentDto>
    {
        public CreateTestAppointmentValidator()
        {
            RuleFor(x => x.TestTypeID)
                .GreaterThan(0).WithMessage("Test Type ID must be greater than 0.");

            RuleFor(x => x.LocalDrivingLicenseApplicationID)
                .GreaterThan(0).WithMessage("Local Driving License Application ID must be greater than 0.");

            RuleFor(x => x.PaidFees)
                .GreaterThanOrEqualTo(0).WithMessage("Paid fees cannot be negative.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }

    public class UpdateTestAppointmentValidator : AbstractValidator<UpdateTestAppointmentDto>
    {
        public UpdateTestAppointmentValidator()
        {
            RuleFor(x => x.PaidFees)
                .GreaterThanOrEqualTo(0).WithMessage("Paid fees cannot be negative.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }
}

