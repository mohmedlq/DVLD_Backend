using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class LicenseClassesRepository : ILicenseClassesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public LicenseClassesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(LicenseClass entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.LicenseClasses.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<LicenseClass>> GetAllAsync()
        {
            return await _context.LicenseClasses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LicenseClass?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.LicenseClasses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LicenseClassID == id);
        }

        public async Task<LicenseClass?> GetByClassNameAsync(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return null;
            }

            return await _context.LicenseClasses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClassName == className);
        }

        #endregion

        #region Exists

        public async Task<bool> ClassNameExistsAsync(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return false;
            }

            return await _context.LicenseClasses
                .AnyAsync(x => x.ClassName == className);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(LicenseClass entity)
        {
            if (entity is null || entity.LicenseClassID <= 0)
            {
                return false;
            }

            var licenseClass = await _context.LicenseClasses
                .FirstOrDefaultAsync(x => x.LicenseClassID == entity.LicenseClassID);

            if (licenseClass is null)
            {
                return false;
            }

            licenseClass.ClassName = entity.ClassName;
            licenseClass.ClassDescription = entity.ClassDescription;
            licenseClass.MinimumAllowedAge = entity.MinimumAllowedAge;
            licenseClass.DefaultValidityLength = entity.DefaultValidityLength;
            licenseClass.ClassFees = entity.ClassFees;

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

            return await _context.LicenseClasses
                .Where(x => x.LicenseClassID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.LicenseClasses.Select(x => x.LicenseClassID).CountAsync();
        }

        #endregion
    }
}
