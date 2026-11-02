using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class TestTypesRepository : ITestTypesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public TestTypesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(TestType entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.TestTypes.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<TestType>> GetAllAsync()
        {
            return await _context.TestTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TestType?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.TestTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TestTypeID == id);
        }

        public async Task<decimal?> GetTestTypeFeesAsync(int testTypeId)
        {
            if (testTypeId <= 0)
            {
                return null;
            }

            return await _context.TestTypes
                .AsNoTracking()
                .Where(x => x.TestTypeID == testTypeId)
                .Select(x => (decimal?)x.TestTypeFees)
                .FirstOrDefaultAsync();
        }

        #endregion

        #region Exists

        public async Task<bool> TestTypeTitleExistsAsync(string testTypeTitle)
        {
            if (string.IsNullOrWhiteSpace(testTypeTitle))
            {
                return false;
            }

            return await _context.TestTypes
                .AnyAsync(x => x.TestTypeTitle == testTypeTitle);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(TestType entity)
        {
            if (entity is null || entity.TestTypeID <= 0)
            {
                return false;
            }

            var testType = await _context.TestTypes
                .FirstOrDefaultAsync(x => x.TestTypeID == entity.TestTypeID);

            if (testType is null)
            {
                return false;
            }

            testType.TestTypeTitle = entity.TestTypeTitle;
            testType.TestTypeDescription = entity.TestTypeDescription;
            testType.TestTypeFees = entity.TestTypeFees;

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

            return await _context.TestTypes
                .Where(x => x.TestTypeID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.TestTypes.Select(x => x.TestTypeID).CountAsync();
        }

        #endregion
    }
}

