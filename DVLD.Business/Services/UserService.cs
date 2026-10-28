using DVLD.Business.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Enums;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Business.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(User entity)
        {
            if (await _usersRepository.UsernameExistsAsync(entity.UserName))
            {
                throw new InvalidOperationException("Username is already taken. Please choose another one.");
            }

            if (entity.PersonID.HasValue && await _usersRepository.ExistsByPersonIdAsync(entity.PersonID.Value))
            {
                throw new InvalidOperationException("This person is already linked to an existing user account.");
            }

            return await _usersRepository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            var existingUser = await _usersRepository.GetByIdAsync(entity.UserID);
            if (existingUser != null && !string.Equals(existingUser.UserName, entity.UserName, StringComparison.OrdinalIgnoreCase))
            {
                if (await _usersRepository.UsernameExistsAsync(entity.UserName))
                {
                    throw new InvalidOperationException("The new username is already taken by another user.");
                }
            }

            return await _usersRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id == 1)
            {
                throw new InvalidOperationException("Action denied: Cannot delete the primary system administrator account.");
            }

            return await _usersRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _usersRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _usersRepository.GetByIdAsync(id);
        }

        #endregion

        #region IUsersService Implementation

        public async Task<User?> GetByPersonIdAsync(int personId)
        {
            return await _usersRepository.GetByPersonIdAsync(personId);
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _usersRepository.GetByUsernameAndPasswordAsync(username, password);
        }

        public async Task<IEnumerable<User>> GetByActiveStateAsync(bool isActive)
        {
            return await _usersRepository.GetByActiveStateAsync(isActive);
        }

        public async Task<bool> ExistsByPersonIdAsync(int personId)
        {
            return await _usersRepository.ExistsByPersonIdAsync(personId);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _usersRepository.UsernameExistsAsync(username);
        }

        public async Task<int> CountAsync()
        {
            return await _usersRepository.CountAsync();
        }

        public async Task<bool> ChangePasswordAsync(int userId, string password)
        {
            // ❌ تم حذف شرط التحقق من طول كلمة المرور (Length < 8) أو كونها فارغة
            // لأن FluentValidation في طبقة الـ API سيقوم برفض الطلب قبل أن يصل إلى هنا

            return await _usersRepository.ChangePasswordAsync(userId, password);
        }

        public async Task<bool> ChangePasswordAsync(string userId, string password)
        {
            // ❌ تم حذف شرط التحقق من طول كلمة المرور هنا أيضاً لنفس السبب

            return await _usersRepository.ChangePasswordAsync(userId, password);
        }

        public async Task<bool> UpdatePermissionsAsync(int userId, enPermissions newPermissions)
        {
            // بزنس لوجيك (قاعدة إدارية) لذلك تبقى هنا
            if (userId == 1 && !newPermissions.HasFlag(enPermissions.All))
            {
                throw new InvalidOperationException("Action denied: Cannot revoke full permissions from the primary system administrator.");
            }

            return await _usersRepository.UpdatePermissionsAsync(userId, newPermissions);
        }

        #endregion
    }
}