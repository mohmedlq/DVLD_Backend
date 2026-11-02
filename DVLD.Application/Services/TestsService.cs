using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class TestsService : ITestsService
    {
        private readonly ITestsRepository _repository;
        private readonly ITestAppointmentsRepository _appointmentsRepository;

        public TestsService(ITestsRepository repository, ITestAppointmentsRepository appointmentsRepository)
        {
            _repository = repository;
            _appointmentsRepository = appointmentsRepository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(Test entity)
        {
            if (entity.TestAppointmentID <= 0)
            {
                throw new InvalidOperationException("Invalid Test Appointment ID.");
            }

            if (entity.CreatedByUserID <= 0)
            {
                throw new InvalidOperationException("Invalid Created By User ID.");
            }

            var existingTest = await _repository.GetByAppointmentIdAsync(entity.TestAppointmentID);
            if (existingTest != null)
            {
                throw new InvalidOperationException("A test has already been recorded for this appointment.");
            }

            var isAdded = await _repository.AddAsync(entity);
            if (isAdded)
            {
                // Lock the appointment once the test has been taken
                await _appointmentsRepository.LockAppointmentAsync(entity.TestAppointmentID);
            }

            return isAdded;
        }

        public async Task<bool> UpdateAsync(Test entity)
        {
            var existing = await _repository.GetByIdAsync(entity.TestID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Test with ID {entity.TestID} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Test with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region ITestsService Implementation

        public async Task<Test?> GetByAppointmentIdAsync(int testAppointmentId)
        {
            return await _repository.GetByAppointmentIdAsync(testAppointmentId);
        }

        public async Task<IEnumerable<Test>> GetPassedTestsAsync()
        {
            return await _repository.GetPassedTestsAsync();
        }

        public async Task<int> GetPassedTestCountAsync(int localDrivingLicenseApplicationId)
        {
            return await _repository.GetPassedTestCountAsync(localDrivingLicenseApplicationId);
        }

        public async Task<bool> HasPassedTestAsync(int localDrivingLicenseApplicationId, int testTypeId)
        {
            return await _repository.HasPassedTestAsync(localDrivingLicenseApplicationId, testTypeId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

