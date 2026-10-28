using DVLD.DataAccess.Context;
using DVLD.Domain.Entities;
using DVLD.Domain.Enums;
using DVLD.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DVLD.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public UsersRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(User entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Users.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserID == id);
        }

        public async Task<User?> GetByPersonIdAsync(int personId)
        {
            if (personId <= 0)
            {
                return null;
            }

            return await _context.Users
                .FirstOrDefaultAsync(x => x.PersonID == personId);
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(
            string username,
            string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.UserName == username &&
                    x.Password == password);
        }

        public async Task<IEnumerable<User>> GetByActiveStateAsync(bool isActive)
        {
            return await _context.Users
                .Where(x => x.IsActive == isActive)
                .ToListAsync();
        }

        #endregion

        #region Exists

        public async Task<bool> ExistsByPersonIdAsync(int personId)
        {
            if (personId <= 0)
            {
                return false;
            }

            return await _context.Users
                .AnyAsync(x => x.PersonID == personId);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            return await _context.Users
                .AnyAsync(x => x.UserName == username);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(User entity)
        {
            if (entity is null || entity.UserID <= 0)
            {
                return false;
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserID == entity.UserID);

            if (user is null)
            {
                return false;
            }

            user.PersonID = entity.PersonID;
            user.UserName = entity.UserName;
            user.IsActive = entity.IsActive;
            
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return await _context.Users
                .Where(x => x.UserID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Users.Select(U=>U.UserID).CountAsync();
        }

        #endregion

        #region Password

        public async Task<bool> ChangePasswordAsync(int userId, string pasword)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(pasword))
            {
                return false;
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserID == userId);

            if (user is null)
            {
                return false;
            }

            user.Password = pasword;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangePasswordAsync(string userId, string pasword)
        {
            if (string.IsNullOrWhiteSpace(userId) ||
                string.IsNullOrWhiteSpace(pasword))
            {
                return false;
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserID.ToString() == userId);

            if (user is null)
            {
                return false;
            }

            user.Password = pasword;

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdatePermissionsAsync(int userId, enPermissions newPermissions)
        {
            if (userId <= 0) return false;

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserID == userId);

            if (user is null) return false;

            user.Permissions = newPermissions;

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}