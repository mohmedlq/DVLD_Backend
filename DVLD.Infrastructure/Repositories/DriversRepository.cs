using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class DriversRepository : IDriversRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public DriversRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(Driver entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Drivers.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _context.Drivers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Driver?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Drivers
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DriverID == id);
        }

        public async Task<Driver?> GetByPersonIdAsync(int personId)
        {
            if (personId <= 0)
            {
                return null;
            }

            return await _context.Drivers
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.PersonID == personId);
        }

        public async Task<List<Driver>> GetByCreatedByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                return new List<Driver>();
            }

            return await _context.Drivers
                .AsNoTracking()
                .Where(d => d.CreatedByUserID == userId)
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

            return await _context.Drivers
                .AnyAsync(d => d.PersonID == personId);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Driver entity)
        {
            if (entity is null || entity.DriverID <= 0)
            {
                return false;
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.DriverID == entity.DriverID);

            if (driver is null)
            {
                return false;
            }

            driver.PersonID = entity.PersonID;
            driver.CreatedByUserID = entity.CreatedByUserID;
            driver.CreatedDate = entity.CreatedDate;

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

            return await _context.Drivers
                .Where(d => d.DriverID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Drivers.Select(d => d.DriverID).CountAsync();
        }

        #endregion
    }
}

