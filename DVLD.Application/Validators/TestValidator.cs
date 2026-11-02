using DVLD.Application.DTOs.Tests;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateTestValidator : AbstractValidator<CreateTestDto>
    {
        public CreateTestValidator()
        {
            RuleFor(x => x.TestAppointmentID)
                .GreaterThan(0).WithMessage("Test Appointment ID must be greater than 0.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.");
        }
    }

    public class UpdateTestValidator : AbstractValidator<UpdateTestDto>
    {
        public UpdateTestValidator()
        {
            RuleFor(x => x.TestAppointmentID)
                .GreaterThan(0).WithMessage("Test Appointment ID must be greater than 0.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.");
        }
    }
}

