using System;
using System.Collections.Generic;

namespace DVLD.Domain.Entities;

public partial class Country
{
    public int CountryID { get; set; }

    public string CountryName { get; set; } = null!;
}
