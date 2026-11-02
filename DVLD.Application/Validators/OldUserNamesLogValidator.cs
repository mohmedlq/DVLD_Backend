using DVLD.Application.DTOs.OldUserNamesLogs;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateOldUserNamesLogValidator : AbstractValidator<CreateOldUserNamesLogDto>
    {
        public CreateOldUserNamesLogValidator()
        {
            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.OldUserName) || !string.IsNullOrWhiteSpace(x.NewUserName))
                .WithMessage("At least one username (old or new) must be provided.");

            RuleFor(x => x.OldUserName)
                .MaximumLength(50).WithMessage("Old username cannot exceed 50 characters.");

            RuleFor(x => x.NewUserName)
                .MaximumLength(50).WithMessage("New username cannot exceed 50 characters.");
        }
    }
}

