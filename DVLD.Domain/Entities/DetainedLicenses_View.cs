using System;
using System.Collections.Generic;

namespace DVLD.Domain.Entities;

public partial class DetainedLicenses_View
{
    public int DetainID { get; set; }

    public int LicenseID { get; set; }

    public DateTime DetainDate { get; set; }

    public bool IsReleased { get; set; }

    public decimal FineFees { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? NationalNo { get; set; }

    public string? FullName { get; set; }

    public int? ReleaseApplicationID { get; set; }
}
