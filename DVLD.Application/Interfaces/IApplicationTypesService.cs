using DVLD.Domain.Entities;
using DVLD.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface IApplicationTypesService
    {
        Task<bool> CreateAsync(ApplicationType entity);

        Task<bool> UpdateAsync(ApplicationType entity);

        Task<bool> DeleteAsync(enApplicationType id);

        Task<IEnumerable<ApplicationType>> GetAllAsync();

        Task<ApplicationType?> GetByIdAsync(enApplicationType id);

        Task<decimal?> GetApplicationFeesAsync(enApplicationType applicationTypeId);

        Task<bool> ApplicationTypeTitleExistsAsync(string applicationTypeTitle);

        Task<int> CountAsync();
    }
}