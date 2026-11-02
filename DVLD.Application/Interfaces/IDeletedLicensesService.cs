using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface IDeletedLicensesService : ICrud<Deleted_License>
    {
        Task<Deleted_License?> GetByLicenseIdAsync(int licenseId);
        Task<IEnumerable<Deleted_License>> GetByDriverIdAsync(int driverId);
        Task<int> CountAsync();
    }
}

