using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class TestsRepository : ITestsRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public TestsRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(Test entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Tests.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Tests
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TestID == id);
        }

        public async Task<Test?> GetByAppointmentIdAsync(int testAppointmentId)
        {
            if (testAppointmentId <= 0)
            {
                return null;
            }

            return await _context.Tests
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TestAppointmentID == testAppointmentId);
        }

        public async Task<IEnumerable<Test>> GetPassedTestsAsync()
        {
            return await _context.Tests
                .AsNoTracking()
                .Where(t => t.TestResult)
                .ToListAsync();
        }

        public async Task<int> GetPassedTestCountAsync(int localDrivingLicenseApplicationId)
        {
            if (localDrivingLicenseApplicationId <= 0)
            {
                return 0;
            }

            return await _context.Tests
                .AsNoTracking()
                .Where(t => t.TestResult && t.TestAppointment.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId)
                .CountAsync();
        }

        #endregion

        #region Exists / Passed

        public async Task<bool> HasPassedTestAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            if (localDrivingLicenseApplicationId <= 0 || testTypeId <= 0)
            {
                return false;
            }

            return await _context.Tests
                .AnyAsync(t => t.TestResult &&
                               t.TestAppointment.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId &&
                               t.TestAppointment.TestTypeID == testTypeId);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Test entity)
        {
            if (entity is null || entity.TestID <= 0)
            {
                return false;
            }

            var test = await _context.Tests
                .FirstOrDefaultAsync(t => t.TestID == entity.TestID);

            if (test is null)
            {
                return false;
            }

            test.TestAppointmentID = entity.TestAppointmentID;
            test.TestResult = entity.TestResult;
            test.Notes = entity.Notes;
            test.CreatedByUserID = entity.CreatedByUserID;

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

            return await _context.Tests
                .Where(t => t.TestID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Tests.Select(t => t.TestID).CountAsync();
        }

        #endregion
    }
}

