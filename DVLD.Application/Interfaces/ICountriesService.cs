using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface ICountriesService : ICrud<Country>
    {
        Task<bool> CountryNameExistsAsync(string countryName);
        Task<int> CountAsync();
    }
}

