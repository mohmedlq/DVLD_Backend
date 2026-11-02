using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface ITestsService : ICrud<Test>
    {
        Task<Test?> GetByAppointmentIdAsync(int testAppointmentId);
        Task<IEnumerable<Test>> GetPassedTestsAsync();
        Task<int> GetPassedTestCountAsync(int localDrivingLicenseApplicationId);
        Task<bool> HasPassedTestAsync(int localDrivingLicenseApplicationId, int testTypeId);
        Task<int> CountAsync();
    }
}

