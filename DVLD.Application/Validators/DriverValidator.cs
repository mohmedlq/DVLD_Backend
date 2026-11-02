using DVLD.Application.DTOs.Drivers;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateDriverValidator : AbstractValidator<CreateDriverDto>
    {
        public CreateDriverValidator()
        {
            RuleFor(x => x.PersonID)
                .GreaterThan(0).WithMessage("Person ID must be greater than 0.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }

    public class UpdateDriverValidator : AbstractValidator<UpdateDriverDto>
    {
        public UpdateDriverValidator()
        {
            RuleFor(x => x.PersonID)
                .GreaterThan(0).WithMessage("Person ID must be greater than 0.");

            RuleFor(x => x.CreatedByUserID)
                .GreaterThan(0).WithMessage("Created By User ID must be greater than 0.");
        }
    }
}

