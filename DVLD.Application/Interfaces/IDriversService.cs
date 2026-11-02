using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface IDriversService : ICrud<Driver>
    {
        Task<Driver?> GetByPersonIdAsync(int personId);
        Task<bool> ExistsByPersonIdAsync(int personId);
        Task<int> CountAsync();
        Task<List<Driver>> GetByCreatedByUserIdAsync(int userId);
    }
}

