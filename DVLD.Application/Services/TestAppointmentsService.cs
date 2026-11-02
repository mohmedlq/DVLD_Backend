using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class TestAppointmentsService : ITestAppointmentsService
    {
        private readonly ITestAppointmentsRepository _repository;

        public TestAppointmentsService(ITestAppointmentsRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(TestAppointment entity)
        {
            if (entity.TestTypeID <= 0)
            {
                throw new InvalidOperationException("Invalid Test Type ID.");
            }

            if (entity.LocalDrivingLicenseApplicationID <= 0)
            {
                throw new InvalidOperationException("Invalid Local Driving License Application ID.");
            }

            if (entity.CreatedByUserID <= 0)
            {
                throw new InvalidOperationException("Invalid Created By User ID.");
            }

            if (await _repository.HasActiveAppointmentAsync(entity.LocalDrivingLicenseApplicationID, entity.TestTypeID))
            {
                throw new InvalidOperationException("This applicant already has an active (unlocked) appointment for this test type.");
            }

            if (entity.AppointmentDate == default)
            {
                entity.AppointmentDate = DateTime.Now;
            }

            entity.IsLocked = false;

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(TestAppointment entity)
        {
            var existing = await _repository.GetByIdAsync(entity.TestAppointmentID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Test Appointment with ID {entity.TestAppointmentID} was not found.");
            }

            if (existing.IsLocked)
            {
                throw new InvalidOperationException("Cannot update a locked test appointment.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Test Appointment with ID {id} was not found.");
            }

            if (existing.IsLocked)
            {
                throw new InvalidOperationException("Cannot delete a locked test appointment.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TestAppointment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TestAppointment?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region ITestAppointmentsService Implementation

        public async Task<IEnumerable<TestAppointment>> GetByLocalDrivingLicenseApplicationIdAsync(int localDrivingLicenseApplicationId)
        {
            return await _repository.GetByLocalDrivingLicenseApplicationIdAsync(localDrivingLicenseApplicationId);
        }

        public async Task<IEnumerable<TestAppointment>> GetByTestTypeIdAsync(int testTypeId)
        {
            return await _repository.GetByTestTypeIdAsync(testTypeId);
        }

        public async Task<TestAppointment?> GetLastTestAppointmentAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            return await _repository.GetLastTestAppointmentAsync(localDrivingLicenseApplicationId, testTypeId);
        }

        public async Task<bool> HasActiveAppointmentAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            return await _repository.HasActiveAppointmentAsync(localDrivingLicenseApplicationId, testTypeId);
        }

        public async Task<bool> LockAppointmentAsync(int appointmentId)
        {
            var existing = await _repository.GetByIdAsync(appointmentId);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Test Appointment with ID {appointmentId} was not found.");
            }

            return await _repository.LockAppointmentAsync(appointmentId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

