namespace DVLD.Business.DTOs.People
{
    public class PersonResponseDto
    {
        public int PersonID { get; set; }
        public string? NationalNo { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte? Gender { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? CountryID { get; set; }
        public string? ImagePath { get; set; }
        public string UserName { get; set; } = null!;
    }

    public class CreatePersonDto
    {
        public string? NationalNo { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte? Gender { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? CountryID { get; set; }
        public string? ImagePath { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UpdatePersonDto
    {
        public string? NationalNo { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte? Gender { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? CountryID { get; set; }
        public string? ImagePath { get; set; }
        public string UserName { get; set; } = null!;
    }

    public class ChangePasswordDto
    {
        public string NewPassword { get; set; } = null!;
    }
}