using System;

namespace DVLD.Application.DTOs.Drivers
{
    public class DriverResponseDto
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CreateDriverDto
    {
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
    }

    public class UpdateDriverDto
    {
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
    }
}

