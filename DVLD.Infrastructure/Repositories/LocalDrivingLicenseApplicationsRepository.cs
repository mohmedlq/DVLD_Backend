using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class LocalDrivingLicenseApplicationsRepository : ILocalDrivingLicenseApplicationsRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public LocalDrivingLicenseApplicationsRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(LocalDrivingLicenseApplication entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.LocalDrivingLicenseApplications.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<LocalDrivingLicenseApplication>> GetAllAsync()
        {
            return await _context.LocalDrivingLicenseApplications
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LocalDrivingLicenseApplication?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.LocalDrivingLicenseApplications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LocalDrivingLicenseApplicationID == id);
        }

        public async Task<LocalDrivingLicenseApplication?> GetByApplicationIdAsync(int applicationId)
        {
            if (applicationId <= 0)
            {
                return null;
            }

            return await _context.LocalDrivingLicenseApplications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ApplicationID == applicationId);
        }

        #endregion

        #region Exists

        public async Task<bool> ExistsByApplicationIdAsync(int applicationId)
        {
            if (applicationId <= 0)
            {
                return false;
            }

            return await _context.LocalDrivingLicenseApplications
                .AnyAsync(x => x.ApplicationID == applicationId);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(LocalDrivingLicenseApplication entity)
        {
            if (entity is null || entity.LocalDrivingLicenseApplicationID <= 0)
            {
                return false;
            }

            var app = await _context.LocalDrivingLicenseApplications
                .FirstOrDefaultAsync(x => x.LocalDrivingLicenseApplicationID == entity.LocalDrivingLicenseApplicationID);

            if (app is null)
            {
                return false;
            }

            app.ApplicationID = entity.ApplicationID;
            app.LicenseClassID = entity.LicenseClassID;

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

            return await _context.LocalDrivingLicenseApplications
                .Where(x => x.LocalDrivingLicenseApplicationID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.LocalDrivingLicenseApplications.Select(x => x.LocalDrivingLicenseApplicationID).CountAsync();
        }

        #endregion
    }
}

