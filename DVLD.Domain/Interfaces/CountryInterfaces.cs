using DVLD.Domain.Entities;

namespace DVLD.Domain.Interfaces
{
    public interface ICountriesRepository : ICrud<Country>
    {
        Task<bool> CountryNameExistsAsync(string countryName);
        Task<int> CountAsync();
    }
}
