using DVLD.Application.DTOs.Massages;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateMassageValidator : AbstractValidator<CreateMassageDto>
    {
        public CreateMassageValidator()
        {
            RuleFor(x => x.PersonID)
                .GreaterThan(0).WithMessage("Person ID must be greater than 0.");

            RuleFor(x => x.MessageTitle)
                .NotEmpty().WithMessage("Message title is required.")
                .MaximumLength(150).WithMessage("Message title cannot exceed 150 characters.");

            RuleFor(x => x.MessageContent)
                .NotEmpty().WithMessage("Message content is required.")
                .MaximumLength(1000).WithMessage("Message content cannot exceed 1000 characters.");
        }
    }

    public class UpdateMassageValidator : AbstractValidator<UpdateMassageDto>
    {
        public UpdateMassageValidator()
        {
            RuleFor(x => x.MessageTitle)
                .NotEmpty().WithMessage("Message title is required.")
                .MaximumLength(150).WithMessage("Message title cannot exceed 150 characters.");

            RuleFor(x => x.MessageContent)
                .NotEmpty().WithMessage("Message content is required.")
                .MaximumLength(1000).WithMessage("Message content cannot exceed 1000 characters.");
        }
    }
}

