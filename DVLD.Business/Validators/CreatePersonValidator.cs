using DVLD.Business.DTOs.People;
using FluentValidation;
using System;

namespace DVLD.Business.Validators
{
    public class CreatePersonValidator : AbstractValidator<CreatePersonDto>
    {
        public CreatePersonValidator()
        {
            RuleFor(x => x.NationalNo)
                .NotEmpty().WithMessage("National Number is required.")
                .MaximumLength(11).WithMessage("National Number cannot exceed 20 characters.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.");

            RuleFor(x => x.SecondName)
                .MaximumLength(20).WithMessage("Second Name cannot exceed 50 characters.");

            RuleFor(x => x.ThirdName)
                .MaximumLength(20).WithMessage("Third Name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(20).WithMessage("Last Name cannot exceed 50 characters.");

            RuleFor(x => x.Gender)
                .Must(g => g == 0 || g == 1).WithMessage("Gender must be 0 (Male) or 1 (Female).");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.CountryID)
                .NotNull().WithMessage("Country is required.")
                .GreaterThan(0).WithMessage("Invalid Country ID.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        }
    }

    public class UpdatePersonValidator : AbstractValidator<UpdatePersonDto>
    {
        public UpdatePersonValidator()
        {
            RuleFor(x => x.NationalNo)
                .NotEmpty().WithMessage("National Number is required.")
                .MaximumLength(20).WithMessage("National Number cannot exceed 20 characters.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.");

            RuleFor(x => x.SecondName)
                .MaximumLength(50).WithMessage("Second Name cannot exceed 50 characters.");

            RuleFor(x => x.ThirdName)
                .MaximumLength(50).WithMessage("Third Name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters.");

            RuleFor(x => x.DateOfBirth)
                .NotNull().WithMessage("Date of Birth is required.");

            RuleFor(x => x.Gender)
                .NotNull().WithMessage("Gender is required.")
                .Must(g => g == 0 || g == 1).WithMessage("Gender must be 0 (Male) or 1 (Female).");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.CountryID)
                .NotNull().WithMessage("Country is required.")
                .GreaterThan(0).WithMessage("Invalid Country ID.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");
        }
    }

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        }
    }
}