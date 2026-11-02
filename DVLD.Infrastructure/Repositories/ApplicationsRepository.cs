using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class ApplicationsRepository : IApplicationsRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public ApplicationsRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(Application entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Applications.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            return await _context.Applications
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Application?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ApplicationID == id);
        }

        public async Task<Application?> GetByApplicantPersonIdAsync(int personId)
        {
            if (personId <= 0)
            {
                return null;
            }

            return await _context.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ApplicantPersonID == personId);
        }

        public async Task<Application?> GetByApplicationTypeIdAsync(int applicationTypeId)
        {
            if (applicationTypeId <= 0)
            {
                return null;
            }

            return await _context.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ApplicationTypeID == applicationTypeId);
        }

        public async Task<IEnumerable<Application>> AllApplicationsForUserId(int userId)
        {
            if (userId <= 0)
            {
                return new List<Application>();
            }

            return await _context.Applications
                .AsNoTracking()
                .Where(x => x.CreatedByUserID == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Application>> GetByStatusAsync(byte applicationStatus)
        {
            return await _context.Applications
                .AsNoTracking()
                .Where(x => x.ApplicationStatus == applicationStatus)
                .ToListAsync();
        }

        #endregion

        #region Exists

        public async Task<bool> ExistsAsync(int applicantPersonId, int applicationTypeId)
        {
            return await _context.Applications
                .AnyAsync(x => x.ApplicantPersonID == applicantPersonId && x.ApplicationTypeID == applicationTypeId);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Application entity)
        {
            if (entity is null || entity.ApplicationID <= 0)
            {
                return false;
            }

            var application = await _context.Applications
                .FirstOrDefaultAsync(x => x.ApplicationID == entity.ApplicationID);

            if (application is null)
            {
                return false;
            }

            application.ApplicantPersonID = entity.ApplicantPersonID;
            application.ApplicationDate = entity.ApplicationDate;
            application.ApplicationTypeID = entity.ApplicationTypeID;
            application.ApplicationStatus = entity.ApplicationStatus;
            application.LastStatusDate = entity.LastStatusDate;
            application.PaidFees = entity.PaidFees;
            application.CreatedByUserID = entity.CreatedByUserID;

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

            return await _context.Applications
                .Where(x => x.ApplicationID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Applications.Select(x => x.ApplicationID).CountAsync();
        }

        #endregion
    }
}
