using DVLD.Domain.Entities;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface ITestTypesRepository : ICrud<TestType>
    {
        Task<decimal?> GetTestTypeFeesAsync(int testTypeId);
        Task<bool> TestTypeTitleExistsAsync(string testTypeTitle);
        Task<int> CountAsync();
    }
}

