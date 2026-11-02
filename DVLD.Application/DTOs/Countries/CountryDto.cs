namespace DVLD.Application.DTOs.Countries
{
    public class CountryResponseDto
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; } = null!;
    }

    public class CreateCountryDto
    {
        public string CountryName { get; set; } = null!;
    }

    public class UpdateCountryDto
    {
        public string CountryName { get; set; } = null!;
    }
}

