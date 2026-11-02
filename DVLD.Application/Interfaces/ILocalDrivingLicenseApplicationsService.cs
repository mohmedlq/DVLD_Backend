using DVLD.Application.DTOs.LocalDrivingLicenseApplications;
using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface ILocalDrivingLicenseApplicationsService
    {
        #region CRUD Operations

        Task<bool> CreateAsync(CreateLocalDrivingLicenseApplicationRequest request);

        Task<bool> UpdateAsync(LocalDrivingLicenseApplication entity);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<LocalDrivingLicenseApplication>> GetAllAsync();

        Task<LocalDrivingLicenseApplication?> GetByIdAsync(int id);

        #endregion

        #region Local Driving License Application Operations

        Task<LocalDrivingLicenseApplication?> GetByApplicationIdAsync(int applicationId);

        Task<bool> ExistsByApplicationIdAsync(int applicationId);

        Task<int> CountAsync();

        #endregion
    }
}