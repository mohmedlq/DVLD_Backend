using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
public interface IDriversRepository : ICrud<Driver>
{
    Task<Driver?> GetByPersonIdAsync(int personId);

    Task<bool> ExistsByPersonIdAsync(int personId);

    Task<int> CountAsync();

    Task<List<Driver>> GetByCreatedByUserIdAsync(int userId);
}