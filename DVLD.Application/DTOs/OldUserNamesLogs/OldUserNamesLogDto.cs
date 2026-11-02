namespace DVLD.Application.DTOs.OldUserNamesLogs
{
    public class OldUserNamesLogResponseDto
    {
        public int LogID { get; set; }
        public string? OldUserName { get; set; }
        public string? NewUserName { get; set; }
    }

    public class CreateOldUserNamesLogDto
    {
        public string? OldUserName { get; set; }
        public string? NewUserName { get; set; }
    }
}

