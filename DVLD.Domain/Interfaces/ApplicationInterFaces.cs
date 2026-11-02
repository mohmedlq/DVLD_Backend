namespace DVLD.Domain.Interfaces;
using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IApplicationsRepository : ICrud<Application>
{
    Task<Application?> GetByApplicantPersonIdAsync(int personId);
    Task<Application?> GetByApplicationTypeIdAsync(int applicationTypeId);
    Task<IEnumerable<Application>> AllApplicationsForUserId(int userId);
    Task<IEnumerable<Application>> GetByStatusAsync(byte applicationStatus);
    Task<bool> ExistsAsync(int applicantPersonId, int applicationTypeId);
    Task<int> CountAsync();
}
