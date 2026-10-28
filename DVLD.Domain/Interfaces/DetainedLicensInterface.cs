using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
public interface IDetainedLicensesRepository
    : ICrud<DetainedLicense>
{

    Task<List<DetainedLicense>> GetByLicenseIdAsync(
        int licenseId);

    Task<List<DetainedLicense>> GetByCreatedByUserIdAsync(
        int userId);

    Task<List<DetainedLicense>> GetReleasedAsync();

    Task<List<DetainedLicense>> GetUnreleasedAsync();

    Task<bool> ExistsByLicenseIdAsync(
        int licenseId);

    Task<int> CountAsync();

}


