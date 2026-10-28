using System;
using System.Collections.Generic;

namespace DVLD.Domain.Entities;

public partial class OldUserNamesLog
{
    public int LogID { get; set; }

    public string? OldUserName { get; set; }

    public string? NewUserName { get; set; }
}
