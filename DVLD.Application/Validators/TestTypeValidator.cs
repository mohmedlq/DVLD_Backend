using DVLD.Application.DTOs.TestTypes;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateTestTypeValidator : AbstractValidator<CreateTestTypeDto>
    {
        public CreateTestTypeValidator()
        {
            RuleFor(x => x.TestTypeTitle)
                .NotEmpty().WithMessage("Test type title is required.")
                .MaximumLength(100).WithMessage("Test type title cannot exceed 100 characters.");

            RuleFor(x => x.TestTypeDescription)
                .NotEmpty().WithMessage("Test type description is required.")
                .MaximumLength(500).WithMessage("Test type description cannot exceed 500 characters.");

            RuleFor(x => x.TestTypeFees)
                .GreaterThanOrEqualTo(0).WithMessage("Test type fees must be greater than or equal to 0.");
        }
    }

    public class UpdateTestTypeValidator : AbstractValidator<UpdateTestTypeDto>
    {
        public UpdateTestTypeValidator()
        {
            RuleFor(x => x.TestTypeTitle)
                .NotEmpty().WithMessage("Test type title is required.")
                .MaximumLength(100).WithMessage("Test type title cannot exceed 100 characters.");

            RuleFor(x => x.TestTypeDescription)
                .NotEmpty().WithMessage("Test type description is required.")
                .MaximumLength(500).WithMessage("Test type description cannot exceed 500 characters.");

            RuleFor(x => x.TestTypeFees)
                .GreaterThanOrEqualTo(0).WithMessage("Test type fees must be greater than or equal to 0.");
        }
    }
}

