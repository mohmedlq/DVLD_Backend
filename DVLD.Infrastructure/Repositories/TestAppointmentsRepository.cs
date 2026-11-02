using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class TestAppointmentsRepository : ITestAppointmentsRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public TestAppointmentsRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(TestAppointment entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.TestAppointments.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<TestAppointment>> GetAllAsync()
        {
            return await _context.TestAppointments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TestAppointment?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.TestAppointments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TestAppointmentID == id);
        }

        public async Task<IEnumerable<TestAppointment>> GetByLocalDrivingLicenseApplicationIdAsync(int localDrivingLicenseApplicationId)
        {
            if (localDrivingLicenseApplicationId <= 0)
            {
                return new List<TestAppointment>();
            }

            return await _context.TestAppointments
                .AsNoTracking()
                .Where(x => x.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId)
                .OrderByDescending(x => x.TestAppointmentID)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestAppointment>> GetByTestTypeIdAsync(int testTypeId)
        {
            if (testTypeId <= 0)
            {
                return new List<TestAppointment>();
            }

            return await _context.TestAppointments
                .AsNoTracking()
                .Where(x => x.TestTypeID == testTypeId)
                .ToListAsync();
        }

        public async Task<TestAppointment?> GetLastTestAppointmentAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            if (localDrivingLicenseApplicationId <= 0 || testTypeId <= 0)
            {
                return null;
            }

            return await _context.TestAppointments
                .AsNoTracking()
                .Where(x => x.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId && x.TestTypeID == testTypeId)
                .OrderByDescending(x => x.TestAppointmentID)
                .FirstOrDefaultAsync();
        }

        #endregion

        #region Exists / Active

        public async Task<bool> HasActiveAppointmentAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            if (localDrivingLicenseApplicationId <= 0 || testTypeId <= 0)
            {
                return false;
            }

            return await _context.TestAppointments
                .AnyAsync(x => x.LocalDrivingLicenseApplicationID == localDrivingLicenseApplicationId &&
                               x.TestTypeID == testTypeId &&
                               !x.IsLocked);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(TestAppointment entity)
        {
            if (entity is null || entity.TestAppointmentID <= 0)
            {
                return false;
            }

            var appointment = await _context.TestAppointments
                .FirstOrDefaultAsync(x => x.TestAppointmentID == entity.TestAppointmentID);

            if (appointment is null)
            {
                return false;
            }

            appointment.TestTypeID = entity.TestTypeID;
            appointment.LocalDrivingLicenseApplicationID = entity.LocalDrivingLicenseApplicationID;
            appointment.AppointmentDate = entity.AppointmentDate;
            appointment.PaidFees = entity.PaidFees;
            appointment.CreatedByUserID = entity.CreatedByUserID;
            appointment.IsLocked = entity.IsLocked;
            appointment.RetakeTestApplicationID = entity.RetakeTestApplicationID;

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

            return await _context.TestAppointments
                .Where(x => x.TestAppointmentID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.TestAppointments.Select(x => x.TestAppointmentID).CountAsync();
        }

        #endregion

        #region Lock

        public async Task<bool> LockAppointmentAsync(int appointmentId)
        {
            if (appointmentId <= 0)
            {
                return false;
            }

            var appointment = await _context.TestAppointments
                .FirstOrDefaultAsync(x => x.TestAppointmentID == appointmentId);

            if (appointment is null)
            {
                return false;
            }

            appointment.IsLocked = true;
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion
    }
}

