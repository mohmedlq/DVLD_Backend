using DVLD.Application.DTOs.Countries;
using FluentValidation;

namespace DVLD.Application.Validators
{
    public class CreateCountryValidator : AbstractValidator<CreateCountryDto>
    {
        public CreateCountryValidator()
        {
            RuleFor(x => x.CountryName)
                .NotEmpty().WithMessage("Country name is required.")
                .MinimumLength(2).WithMessage("Country name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Country name cannot exceed 50 characters.");
        }
    }

    public class UpdateCountryValidator : AbstractValidator<UpdateCountryDto>
    {
        public UpdateCountryValidator()
        {
            RuleFor(x => x.CountryName)
                .NotEmpty().WithMessage("Country name is required.")
                .MinimumLength(2).WithMessage("Country name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Country name cannot exceed 50 characters.");
        }
    }
}

