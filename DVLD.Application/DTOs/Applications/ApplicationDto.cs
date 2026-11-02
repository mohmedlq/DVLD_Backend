using System;

namespace DVLD.Application.DTOs.Applications
{
    public class ApplicationResponseDto
    {
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class CreateApplicationDto
    {
        public int ApplicantPersonID { get; set; }
        public int ApplicationTypeID { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class UpdateApplicationDto
    {
        public int ApplicantPersonID { get; set; }
        public int ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
    }
}

