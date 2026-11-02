using System;

namespace DVLD.Application.DTOs.DetainedLicenses
{
    public class DetainedLicenseResponseDto
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        public int? ReleaseApplicationID { get; set; }
    }

    public class CreateDetainedLicenseDto
    {
        public int LicenseID { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class ReleaseDetainedLicenseDto
    {
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
    }

    public class UpdateDetainedLicenseDto
    {
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
    }
}

