using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface IInternationalLicensesService : ICrud<InternationalLicense>
    {
        Task<IEnumerable<InternationalLicense>> GetDriverInternationalLicensesAsync(int driverId);
        Task<InternationalLicense?> GetActiveByDriverIdAsync(int driverId);
        Task<bool> HasActiveInternationalLicenseAsync(int driverId);
        Task<int> CountAsync();
    }
}

