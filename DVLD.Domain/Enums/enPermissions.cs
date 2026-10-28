using System;

namespace DVLD.Domain.Enums
{
    [Flags]
    public enum enPermissions
    {
        None = 0,
        View = 1,
        Add = 2,
        Update = 4,
        Delete = 8,
        ManageUsers = 16,
        All = -1,
        CreateLicenses= 32,
        DetainLicenses= 64,
        ReleaseLicenses= 128,
        ManageApplications= 256,
        ManageTests= 512,

    }
}