using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface ITestsRepository : ICrud<Test>
    {
        Task<Test?> GetByAppointmentIdAsync(int testAppointmentId);
        Task<IEnumerable<Test>> GetPassedTestsAsync();
        Task<int> GetPassedTestCountAsync(int localDrivingLicenseApplicationId);
        Task<bool> HasPassedTestAsync(int localDrivingLicenseApplicationId, int testTypeId);
        Task<int> CountAsync();
    }
}

