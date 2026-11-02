namespace DVLD.Application.DTOs.Tests
{
    public class TestResponseDto
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string? Notes { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class CreateTestDto
    {
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string? Notes { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class UpdateTestDto
    {
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string? Notes { get; set; }
        public int CreatedByUserID { get; set; }
    }
}

