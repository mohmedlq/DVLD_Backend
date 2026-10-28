using DVLD.Domain.Entities;
using DVLD.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Business.Interfaces
{
    public interface IUsersService : ICrud<User>
    {

        Task<User?> GetByPersonIdAsync(int personId);
        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);
        Task<IEnumerable<User>> GetByActiveStateAsync(bool isActive);

        Task<bool> ExistsByPersonIdAsync(int personId);
        Task<bool> UsernameExistsAsync(string username);

        Task<int> CountAsync();

        Task<bool> ChangePasswordAsync(int userId, string password);
        Task<bool> ChangePasswordAsync(string userId, string password);
        Task<bool> UpdatePermissionsAsync(int userId, enPermissions newPermissions);
    }
}