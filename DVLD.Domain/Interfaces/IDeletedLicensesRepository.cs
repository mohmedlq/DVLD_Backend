using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface IDeletedLicensesRepository : ICrud<Deleted_License>
    {
        Task<Deleted_License?> GetByLicenseIdAsync(int licenseId);
        Task<IEnumerable<Deleted_License>> GetByDriverIdAsync(int driverId);
        Task<int> CountAsync();
    }
}

