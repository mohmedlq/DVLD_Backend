using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface ILocalDrivingLicenseApplicationsRepository : ICrud<LocalDrivingLicenseApplication>
    {
        Task<LocalDrivingLicenseApplication?> GetByApplicationIdAsync(int applicationId);
        Task<bool> ExistsByApplicationIdAsync(int applicationId);
        Task<int> CountAsync();
    }
}

