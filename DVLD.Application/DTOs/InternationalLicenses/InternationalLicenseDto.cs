using System;

namespace DVLD.Application.DTOs.InternationalLicenses
{
    public class InternationalLicenseResponseDto
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class CreateInternationalLicenseDto
    {
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class UpdateInternationalLicenseDto
    {
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}

