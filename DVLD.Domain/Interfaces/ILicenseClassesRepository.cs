using DVLD.Domain.Entities;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface ILicenseClassesRepository : ICrud<LicenseClass>
    {
        Task<LicenseClass?> GetByClassNameAsync(string className);
        Task<bool> ClassNameExistsAsync(string className);
        Task<int> CountAsync();
    }
}

