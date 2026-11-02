using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class DetainedLicensesRepository : IDetainedLicensesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public DetainedLicensesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(DetainedLicense entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.DetainedLicenses.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<DetainedLicense>> GetAllAsync()
        {
            return await _context.DetainedLicenses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DetainedLicense?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.DetainedLicenses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DetainID == id);
        }

        public async Task<List<DetainedLicense>> GetByLicenseIdAsync(int licenseId)
        {
            if (licenseId <= 0)
            {
                return new List<DetainedLicense>();
            }

            return await _context.DetainedLicenses
                .AsNoTracking()
                .Where(x => x.LicenseID == licenseId)
                .OrderByDescending(x => x.DetainID)
                .ToListAsync();
        }

        public async Task<List<DetainedLicense>> GetByCreatedByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                return new List<DetainedLicense>();
            }

            return await _context.DetainedLicenses
                .AsNoTracking()
                .Where(x => x.CreatedByUserID == userId)
                .ToListAsync();
        }

        public async Task<List<DetainedLicense>> GetReleasedAsync()
        {
            return await _context.DetainedLicenses
                .AsNoTracking()
                .Where(x => x.IsReleased)
                .ToListAsync();
        }

        public async Task<List<DetainedLicense>> GetUnreleasedAsync()
        {
            return await _context.DetainedLicenses
                .AsNoTracking()
                .Where(x => !x.IsReleased)
                .ToListAsync();
        }

        #endregion

        #region Exists / Detained State

        public async Task<bool> ExistsByLicenseIdAsync(int licenseId)
        {
            if (licenseId <= 0)
            {
                return false;
            }

            return await _context.DetainedLicenses
                .AnyAsync(x => x.LicenseID == licenseId);
        }

        public async Task<bool> IsLicenseDetainedAsync(int licenseId)
        {
            if (licenseId <= 0)
            {
                return false;
            }

            return await _context.DetainedLicenses
                .AnyAsync(x => x.LicenseID == licenseId && !x.IsReleased);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(DetainedLicense entity)
        {
            if (entity is null || entity.DetainID <= 0)
            {
                return false;
            }

            var detained = await _context.DetainedLicenses
                .FirstOrDefaultAsync(x => x.DetainID == entity.DetainID);

            if (detained is null)
            {
                return false;
            }

            detained.LicenseID = entity.LicenseID;
            detained.DetainDate = entity.DetainDate;
            detained.FineFees = entity.FineFees;
            detained.CreatedByUserID = entity.CreatedByUserID;
            detained.IsReleased = entity.IsReleased;
            detained.ReleaseDate = entity.ReleaseDate;
            detained.ReleasedByUserID = entity.ReleasedByUserID;
            detained.ReleaseApplicationID = entity.ReleaseApplicationID;

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

            return await _context.DetainedLicenses
                .Where(x => x.DetainID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Release

        public async Task<bool> ReleaseDetainedLicenseAsync(int detainId, int releasedByUserId, int releaseApplicationId)
        {
            if (detainId <= 0 || releasedByUserId <= 0 || releaseApplicationId <= 0)
            {
                return false;
            }

            var detained = await _context.DetainedLicenses
                .FirstOrDefaultAsync(x => x.DetainID == detainId);

            if (detained is null || detained.IsReleased)
            {
                return false;
            }

            detained.IsReleased = true;
            detained.ReleaseDate = DateTime.Now;
            detained.ReleasedByUserID = releasedByUserId;
            detained.ReleaseApplicationID = releaseApplicationId;

            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.DetainedLicenses.Select(x => x.DetainID).CountAsync();
        }

        #endregion
    }
}

