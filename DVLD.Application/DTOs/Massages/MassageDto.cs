namespace DVLD.Application.DTOs.Massages
{
    public class MassageResponseDto
    {
        public int MessageID { get; set; }
        public int PersonID { get; set; }
        public string MessageTitle { get; set; } = null!;
        public string MessageContent { get; set; } = null!;
        public bool IsRead { get; set; }
    }

    public class CreateMassageDto
    {
        public int PersonID { get; set; }
        public string MessageTitle { get; set; } = null!;
        public string MessageContent { get; set; } = null!;
    }

    public class UpdateMassageDto
    {
        public string MessageTitle { get; set; } = null!;
        public string MessageContent { get; set; } = null!;
        public bool IsRead { get; set; }
    }
}

