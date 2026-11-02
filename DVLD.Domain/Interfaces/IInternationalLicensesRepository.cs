using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface IInternationalLicensesRepository : ICrud<InternationalLicense>
    {
        Task<IEnumerable<InternationalLicense>> GetDriverInternationalLicensesAsync(int driverId);
        Task<InternationalLicense?> GetActiveByDriverIdAsync(int driverId);
        Task<bool> HasActiveInternationalLicenseAsync(int driverId);
        Task<int> CountAsync();
    }
}

