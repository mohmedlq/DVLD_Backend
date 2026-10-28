using System;
using System.Collections.Generic;

namespace DVLD.Domain.Entities;

public partial class Person
{
    public int PersonID { get; set; }

    public string? NationalNo { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? ThirdName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// 0 Male , 1 Femail
    /// </summary>
    public byte? Gender { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? CountryID { get; set; }

    public string? ImagePath { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Massage> Massages { get; set; } = new List<Massage>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
