using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface ILicenseClassesService : ICrud<LicenseClass>
    {
        Task<LicenseClass?> GetByClassNameAsync(string className);
        Task<bool> ClassNameExistsAsync(string className);
        Task<int> CountAsync();
    }
}

