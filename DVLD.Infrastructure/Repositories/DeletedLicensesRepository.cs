using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class DeletedLicensesRepository : IDeletedLicensesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public DeletedLicensesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(Deleted_License entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Deleted_Licenses.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<Deleted_License>> GetAllAsync()
        {
            return await _context.Deleted_Licenses
                .AsNoTracking()
                .OrderByDescending(d => d.DeletedID)
                .ToListAsync();
        }

        public async Task<Deleted_License?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Deleted_Licenses
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DeletedID == id);
        }

        public async Task<Deleted_License?> GetByLicenseIdAsync(int licenseId)
        {
            if (licenseId <= 0)
            {
                return null;
            }

            return await _context.Deleted_Licenses
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.LicenseID == licenseId);
        }

        public async Task<IEnumerable<Deleted_License>> GetByDriverIdAsync(int driverId)
        {
            if (driverId <= 0)
            {
                return new List<Deleted_License>();
            }

            return await _context.Deleted_Licenses
                .AsNoTracking()
                .Where(d => d.DriverID == driverId)
                .OrderByDescending(d => d.DeletedID)
                .ToListAsync();
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Deleted_License entity)
        {
            if (entity is null || entity.DeletedID <= 0)
            {
                return false;
            }

            var deleted = await _context.Deleted_Licenses
                .FirstOrDefaultAsync(d => d.DeletedID == entity.DeletedID);

            if (deleted is null)
            {
                return false;
            }

            deleted.LicenseID = entity.LicenseID;
            deleted.ApplicationID = entity.ApplicationID;
            deleted.DriverID = entity.DriverID;
            deleted.LicenseClass = entity.LicenseClass;
            deleted.IssueDate = entity.IssueDate;
            deleted.ExpirationDate = entity.ExpirationDate;
            deleted.Notes = entity.Notes;
            deleted.PaidFees = entity.PaidFees;
            deleted.IsActive = entity.IsActive;
            deleted.IssueReason = entity.IssueReason;
            deleted.CreatedByUserID = entity.CreatedByUserID;
            deleted.DeletionDate = entity.DeletionDate;

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

            return await _context.Deleted_Licenses
                .Where(d => d.DeletedID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Deleted_Licenses.Select(d => d.DeletedID).CountAsync();
        }

        #endregion
    }
}

