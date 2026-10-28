using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD.Domain.Enums;
using DVLD.Domain.Entities;

namespace DVLD.Domain.Interfaces
{
    public interface IUsersRepository:ICrud<User>
    {
         Task<User?> GetByPersonIdAsync(int personId);

        Task<bool> ExistsByPersonIdAsync(int personId);

        Task<bool> UsernameExistsAsync(string username);

        Task<User?> GetByUsernameAndPasswordAsync(
            string username,
            string password);

        Task<IEnumerable<User>> GetByActiveStateAsync(bool isActive);

        Task<int> CountAsync();

        Task<bool> ChangePasswordAsync(int userId,string pasword);
        Task<bool> ChangePasswordAsync(string userId, string pasword);
         Task<bool> UpdatePermissionsAsync(int userId, enPermissions newPermissions);

    }
}
