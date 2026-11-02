using DVLD.Application.DTOs.Logs;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateLogValidator : AbstractValidator<CreateLogDto>
    {
        public CreateLogValidator()
        {
            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("Log level is required.")
                .MaximumLength(50).WithMessage("Log level cannot exceed 50 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Log message is required.");
        }
    }
}

