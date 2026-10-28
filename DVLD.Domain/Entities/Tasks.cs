using System;
using System.Collections.Generic;

namespace DVLD.Domain.Entities;

public partial class Tasks
{
    public int TaskID { get; set; }

    public int UserID { get; set; }

    public string TaskTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly AssignedDate { get; set; }

    public DateOnly DueDate { get; set; }

    public bool Status { get; set; }

    public virtual User User { get; set; } = null!;
}
