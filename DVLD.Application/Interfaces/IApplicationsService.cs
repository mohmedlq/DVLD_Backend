using AppEntity = DVLD.Domain.Entities.Application;
using DVLD.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface IApplicationsService
    {
        #region CRUD Operations

        Task<int> CreateAsync(AppEntity entity);

        Task<bool> UpdateAsync(AppEntity entity);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<AppEntity>> GetAllAsync();

        Task<AppEntity?> GetByIdAsync(int id);

        #endregion

        #region Application Specific Operations

        Task<AppEntity?> GetByApplicantPersonIdAsync(int personId);

        Task<AppEntity?> GetByApplicationTypeIdAsync(
            enApplicationType applicationTypeId);

        Task<IEnumerable<AppEntity>> AllApplicationsForUserId(int userId);

        Task<IEnumerable<AppEntity>> GetByStatusAsync(
            enApplicationStatus applicationStatus);

        Task<bool> ExistsAsync(
            int applicantPersonId,
            enApplicationType applicationTypeId);

        Task<int> CountAsync();

        Task<bool> CancelAsync(int applicationId);

        Task<bool> SetCompleteAsync(int applicationId);

        #endregion
    }
}