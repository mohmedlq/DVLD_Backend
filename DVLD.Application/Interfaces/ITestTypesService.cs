using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface ITestTypesService : ICrud<TestType>
    {
        Task<decimal?> GetTestTypeFeesAsync(int testTypeId);
        Task<bool> TestTypeTitleExistsAsync(string testTypeTitle);
        Task<int> CountAsync();
    }
}

