using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface ILicensesRepository : ICrud<License>
    {
        Task<IEnumerable<License>> GetDriverLicensesAsync(int driverId);
        Task<License?> GetActiveLicenseByPersonIdAsync(int personId, int licenseClassId);
        Task<bool> HasActiveLicenseAsync(int personId, int licenseClassId);
        Task<bool> DeactivateLicenseAsync(int licenseId);
        Task<int> CountAsync();
    }
}

