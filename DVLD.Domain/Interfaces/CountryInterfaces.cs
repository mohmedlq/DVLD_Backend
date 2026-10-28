using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
public interface ICountriesRepository : ICrud<Country>
{
    Task<bool> CountryNameExistsAsync(string countryName);

    Task<int> CountAsync();
}
