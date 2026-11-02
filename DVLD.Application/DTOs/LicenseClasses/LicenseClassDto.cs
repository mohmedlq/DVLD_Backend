namespace DVLD.Application.DTOs.LicenseClasses
{
    public class LicenseClassResponseDto
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; } = null!;
        public string ClassDescription { get; set; } = null!;
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }
    }

    public class CreateLicenseClassDto
    {
        public string ClassName { get; set; } = null!;
        public string ClassDescription { get; set; } = null!;
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }
    }

    public class UpdateLicenseClassDto
    {
        public string ClassName { get; set; } = null!;
        public string ClassDescription { get; set; } = null!;
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }
    }
}

