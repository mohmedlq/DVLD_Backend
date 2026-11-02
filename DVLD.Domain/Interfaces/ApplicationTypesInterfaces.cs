using DVLD.Domain.Entities;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface IApplicationTypesRepository : ICrud<ApplicationType>
    {
        Task<decimal?> GetApplicationFees(int AppId);
        Task<bool> ApplicationTypeTitleExistsAsync(string applicationTypeTitle);
        Task<int> CountAsync();
    }
}
