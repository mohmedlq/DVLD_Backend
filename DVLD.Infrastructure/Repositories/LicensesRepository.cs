using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class LicensesRepository : ILicensesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public LicensesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(License entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Licenses.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<License>> GetAllAsync()
        {
            return await _context.Licenses
                .AsNoTracking()
                .Where(l => !l.IsDeleted)
                .ToListAsync();
        }

        public async Task<License?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Licenses
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.LicenseID == id && !l.IsDeleted);
        }

        public async Task<IEnumerable<License>> GetDriverLicensesAsync(int driverId)
        {
            if (driverId <= 0)
            {
                return new List<License>();
            }

            return await _context.Licenses
                .AsNoTracking()
                .Where(l => l.DriverID == driverId && !l.IsDeleted)
                .OrderByDescending(l => l.LicenseID)
                .ToListAsync();
        }

        public async Task<License?> GetActiveLicenseByPersonIdAsync(int personId, int licenseClassId)
        {
            if (personId <= 0 || licenseClassId <= 0)
            {
                return null;
            }

            return await _context.Licenses
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Driver.PersonID == personId &&
                                         l.LicenseClass == licenseClassId &&
                                         l.IsActive &&
                                         !l.IsDeleted);
        }

        #endregion

        #region Exists / Active

        public async Task<bool> HasActiveLicenseAsync(int personId, int licenseClassId)
        {
            if (personId <= 0 || licenseClassId <= 0)
            {
                return false;
            }

            return await _context.Licenses
                .AnyAsync(l => l.Driver.PersonID == personId &&
                               l.LicenseClass == licenseClassId &&
                               l.IsActive &&
                               !l.IsDeleted);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(License entity)
        {
            if (entity is null || entity.LicenseID <= 0)
            {
                return false;
            }

            var license = await _context.Licenses
                .FirstOrDefaultAsync(l => l.LicenseID == entity.LicenseID);

            if (license is null)
            {
                return false;
            }

            license.ApplicationID = entity.ApplicationID;
            license.DriverID = entity.DriverID;
            license.LicenseClass = entity.LicenseClass;
            license.IssueDate = entity.IssueDate;
            license.ExpirationDate = entity.ExpirationDate;
            license.Notes = entity.Notes;
            license.PaidFees = entity.PaidFees;
            license.IsActive = entity.IsActive;
            license.IssueReason = entity.IssueReason;
            license.CreatedByUserID = entity.CreatedByUserID;
            license.IsDeleted = entity.IsDeleted;

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

            var license = await _context.Licenses.FirstOrDefaultAsync(l => l.LicenseID == id);
            if (license is null)
            {
                return false;
            }

            // Soft delete
            license.IsDeleted = true;
            license.IsActive = false;
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Deactivate

        public async Task<bool> DeactivateLicenseAsync(int licenseId)
        {
            if (licenseId <= 0)
            {
                return false;
            }

            var license = await _context.Licenses.FirstOrDefaultAsync(l => l.LicenseID == licenseId);
            if (license is null)
            {
                return false;
            }

            license.IsActive = false;
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Licenses.Where(l => !l.IsDeleted).Select(l => l.LicenseID).CountAsync();
        }

        #endregion
    }
}

