namespace DVLD.Application.DTOs.TestTypes
{
    public class TestTypeResponseDto
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; } = null!;
        public string TestTypeDescription { get; set; } = null!;
        public decimal TestTypeFees { get; set; }
    }

    public class CreateTestTypeDto
    {
        public string TestTypeTitle { get; set; } = null!;
        public string TestTypeDescription { get; set; } = null!;
        public decimal TestTypeFees { get; set; }
    }

    public class UpdateTestTypeDto
    {
        public string TestTypeTitle { get; set; } = null!;
        public string TestTypeDescription { get; set; } = null!;
        public decimal TestTypeFees { get; set; }
    }
}

