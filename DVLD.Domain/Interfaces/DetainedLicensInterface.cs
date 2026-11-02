using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface IDetainedLicensesRepository : ICrud<DetainedLicense>
    {
        Task<List<DetainedLicense>> GetByLicenseIdAsync(int licenseId);
        Task<List<DetainedLicense>> GetByCreatedByUserIdAsync(int userId);
        Task<List<DetainedLicense>> GetReleasedAsync();
        Task<List<DetainedLicense>> GetUnreleasedAsync();
        Task<bool> ExistsByLicenseIdAsync(int licenseId);
        Task<bool> IsLicenseDetainedAsync(int licenseId);
        Task<bool> ReleaseDetainedLicenseAsync(int detainId, int releasedByUserId, int releaseApplicationId);
        Task<int> CountAsync();
    }
}
