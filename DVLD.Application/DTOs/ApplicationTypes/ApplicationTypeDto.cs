namespace DVLD.Application.DTOs.ApplicationTypes
{
    public class ApplicationTypeResponseDto
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; } = null!;
        public decimal ApplicationFees { get; set; }
    }

    public class CreateApplicationTypeDto
    {
        public string ApplicationTypeTitle { get; set; } = null!;
        public decimal ApplicationFees { get; set; }
    }

    public class UpdateApplicationTypeDto
    {
        public string ApplicationTypeTitle { get; set; } = null!;
        public decimal ApplicationFees { get; set; }
    }
}

