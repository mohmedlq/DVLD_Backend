using DVLD.Application.DTOs.ApplicationTypes;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateApplicationTypeValidator : AbstractValidator<CreateApplicationTypeDto>
    {
        public CreateApplicationTypeValidator()
        {
            RuleFor(x => x.ApplicationTypeTitle)
                .NotEmpty().WithMessage("Application type title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.ApplicationFees)
                .GreaterThanOrEqualTo(0).WithMessage("Application fees must be greater than or equal to 0.");
        }
    }

    public class UpdateApplicationTypeValidator : AbstractValidator<UpdateApplicationTypeDto>
    {
        public UpdateApplicationTypeValidator()
        {
            RuleFor(x => x.ApplicationTypeTitle)
                .NotEmpty().WithMessage("Application type title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.ApplicationFees)
                .GreaterThanOrEqualTo(0).WithMessage("Application fees must be greater than or equal to 0.");
        }
    }
}

