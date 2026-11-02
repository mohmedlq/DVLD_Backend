using System;

namespace DVLD.Application.DTOs.Tasks
{
    public class TaskResponseDto
    {
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public string TaskTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly AssignedDate { get; set; }
        public DateOnly DueDate { get; set; }
        public bool Status { get; set; }
    }

    public class CreateTaskDto
    {
        public int UserID { get; set; }
        public string TaskTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly DueDate { get; set; }
    }

    public class UpdateTaskDto
    {
        public int UserID { get; set; }
        public string TaskTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly DueDate { get; set; }
        public bool Status { get; set; }
    }
}

