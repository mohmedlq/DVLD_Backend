using DVLD.DataAccess.Context;
using DVLD.Domain;
using DVLD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVLD.DataAccess.Repositories
{
    public class ApplicationTypesRepository : IApplicationTypesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public ApplicationTypesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(ApplicationType entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.ApplicationTypes.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<ApplicationType>> GetAllAsync()
        {
            return await _context.ApplicationTypes.ToListAsync();
        }

        public async Task<ApplicationType?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.ApplicationTypes
                .FirstOrDefaultAsync(x => x.ApplicationTypeID == id);
        }

        public async Task<decimal?> GetApplicationFees(int AppId)
        {
            if (AppId <= 0)
            {
                return null;
            }

            return await _context.ApplicationTypes
                .Where(x => x.ApplicationTypeID == AppId)
                .Select(x => (decimal?)x.ApplicationFees)
                .FirstOrDefaultAsync();
        }

        #endregion

        #region Exists

        public async Task<bool> ApplicationTypeTitleExistsAsync(string applicationTypeTitle)
        {
            if (string.IsNullOrWhiteSpace(applicationTypeTitle))
            {
                return false;
            }

            return await _context.ApplicationTypes
                .AnyAsync(x => x.ApplicationTypeTitle == applicationTypeTitle);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(ApplicationType entity)
        {
            if (entity is null || entity.ApplicationTypeID <= 0)
            {
                return false;
            }
            
            var applicationType = await _context.ApplicationTypes
                .FirstOrDefaultAsync(x =>
                    x.ApplicationTypeID == entity.ApplicationTypeID);

            if (applicationType is null)
            {
                return false;
            }

            applicationType.ApplicationTypeTitle = entity.ApplicationTypeTitle;
            applicationType.ApplicationFees = entity.ApplicationFees;

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

            return await _context.ApplicationTypes
                .Where(x => x.ApplicationTypeID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.ApplicationTypes.Select(A=>A.ApplicationTypeID).CountAsync();
        }

        #endregion
    }
}