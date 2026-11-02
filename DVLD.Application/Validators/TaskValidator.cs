using DVLD.Application.DTOs.Tasks;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.UserID)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(x => x.TaskTitle)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(150).WithMessage("Task title cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Task description is required.")
                .MaximumLength(1000).WithMessage("Task description cannot exceed 1000 characters.");
        }
    }

    public class UpdateTaskValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.UserID)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(x => x.TaskTitle)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(150).WithMessage("Task title cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Task description is required.")
                .MaximumLength(1000).WithMessage("Task description cannot exceed 1000 characters.");
        }
    }
}

