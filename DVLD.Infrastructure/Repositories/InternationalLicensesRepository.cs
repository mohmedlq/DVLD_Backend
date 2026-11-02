using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class InternationalLicensesRepository : IInternationalLicensesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public InternationalLicensesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(InternationalLicense entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.InternationalLicenses.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<InternationalLicense>> GetAllAsync()
        {
            return await _context.InternationalLicenses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<InternationalLicense?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.InternationalLicenses
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.InternationalLicenseID == id);
        }

        public async Task<IEnumerable<InternationalLicense>> GetDriverInternationalLicensesAsync(int driverId)
        {
            if (driverId <= 0)
            {
                return new List<InternationalLicense>();
            }

            return await _context.InternationalLicenses
                .AsNoTracking()
                .Where(l => l.DriverID == driverId)
                .OrderByDescending(l => l.InternationalLicenseID)
                .ToListAsync();
        }

        public async Task<InternationalLicense?> GetActiveByDriverIdAsync(int driverId)
        {
            if (driverId <= 0)
            {
                return null;
            }

            return await _context.InternationalLicenses
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.DriverID == driverId && l.IsActive && l.ExpirationDate > System.DateTime.Now);
        }

        #endregion

        #region Exists / Active

        public async Task<bool> HasActiveInternationalLicenseAsync(int driverId)
        {
            if (driverId <= 0)
            {
                return false;
            }

            return await _context.InternationalLicenses
                .AnyAsync(l => l.DriverID == driverId && l.IsActive && l.ExpirationDate > System.DateTime.Now);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(InternationalLicense entity)
        {
            if (entity is null || entity.InternationalLicenseID <= 0)
            {
                return false;
            }

            var license = await _context.InternationalLicenses
                .FirstOrDefaultAsync(l => l.InternationalLicenseID == entity.InternationalLicenseID);

            if (license is null)
            {
                return false;
            }

            license.ApplicationID = entity.ApplicationID;
            license.DriverID = entity.DriverID;
            license.IssuedUsingLocalLicenseID = entity.IssuedUsingLocalLicenseID;
            license.IssueDate = entity.IssueDate;
            license.ExpirationDate = entity.ExpirationDate;
            license.IsActive = entity.IsActive;
            license.CreatedByUserID = entity.CreatedByUserID;

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

            return await _context.InternationalLicenses
                .Where(l => l.InternationalLicenseID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.InternationalLicenses.Select(l => l.InternationalLicenseID).CountAsync();
        }

        #endregion
    }
}

