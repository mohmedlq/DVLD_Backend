using System;
using System.Collections.Generic;

namespace DVLD.Domain.Entities;

public partial class Massage
{
    public int MessageID { get; set; }

    public int PersonID { get; set; }

    public string MessageTitle { get; set; } = null!;

    public string MessageContent { get; set; } = null!;

    public bool IsRead { get; set; }

    public virtual Person Person { get; set; } = null!;
}
