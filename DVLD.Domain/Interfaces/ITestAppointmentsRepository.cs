using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface ITestAppointmentsRepository : ICrud<TestAppointment>
    {
        Task<IEnumerable<TestAppointment>> GetByLocalDrivingLicenseApplicationIdAsync(int localDrivingLicenseApplicationId);
        Task<IEnumerable<TestAppointment>> GetByTestTypeIdAsync(int testTypeId);
        Task<TestAppointment?> GetLastTestAppointmentAsync(int localDrivingLicenseApplicationId, int testTypeId);
        Task<bool> HasActiveAppointmentAsync(int localDrivingLicenseApplicationId, int testTypeId);
        Task<bool> LockAppointmentAsync(int appointmentId);
        Task<int> CountAsync();
    }
}

