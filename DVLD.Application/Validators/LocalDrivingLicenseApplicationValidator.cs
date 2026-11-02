using DVLD.Application.DTOs.LocalDrivingLicenseApplications;
using FluentValidation;

namespace DVLD.Application.Validators
{
    
    public class UpdateLocalDrivingLicenseApplicationValidator : AbstractValidator<UpdateLocalDrivingLicenseApplicationDto>
    {
        public UpdateLocalDrivingLicenseApplicationValidator()
        {
            RuleFor(x => x.ApplicationID)
                .GreaterThan(0).WithMessage("Application ID must be greater than 0.");

            RuleFor(x => x.LicenseClassID)
                .GreaterThan(0).WithMessage("License Class ID must be greater than 0.");
        }
    }
}

