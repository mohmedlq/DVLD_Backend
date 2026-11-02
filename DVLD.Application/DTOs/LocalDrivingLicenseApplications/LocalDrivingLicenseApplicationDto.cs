namespace DVLD.Application.DTOs.LocalDrivingLicenseApplications
{
    public class LocalDrivingLicenseApplicationResponseDto
    {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }
    }

    public class CreateLocalDrivingLicenseApplicationRequest
    {
        public int ApplicantPersonID { get; set; }
        public byte LicenseClassID { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class UpdateLocalDrivingLicenseApplicationDto
    {
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }
    }
}

