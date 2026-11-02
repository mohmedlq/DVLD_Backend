using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class LicensesService : ILicensesService
    {
        private readonly ILicensesRepository _repository;

        public LicensesService(ILicensesRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(License entity)
        {
            if (entity.ApplicationID <= 0)
            {
                throw new InvalidOperationException("Invalid Application ID.");
            }

            if (entity.DriverID <= 0)
            {
                throw new InvalidOperationException("Invalid Driver ID.");
            }

            if (entity.LicenseClass <= 0)
            {
                throw new InvalidOperationException("Invalid License Class ID.");
            }

            if (entity.CreatedByUserID <= 0)
            {
                throw new InvalidOperationException("Invalid Created By User ID.");
            }

            if (entity.IssueDate == default)
            {
                entity.IssueDate = DateTime.Now;
            }

            if (entity.ExpirationDate <= entity.IssueDate)
            {
                throw new InvalidOperationException("Expiration date must be after the issue date.");
            }

            entity.IsActive = true;
            entity.IsDeleted = false;

            

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(License entity)
        {
            var existing = await _repository.GetByIdAsync(entity.LicenseID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"License with ID {entity.LicenseID} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"License with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<License>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<License?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region ILicensesService Implementation

        public async Task<IEnumerable<License>> GetDriverLicensesAsync(int driverId)
        {
            return await _repository.GetDriverLicensesAsync(driverId);
        }

        public async Task<License?> GetActiveLicenseByPersonIdAsync(int personId, int licenseClassId)
        {
            return await _repository.GetActiveLicenseByPersonIdAsync(personId, licenseClassId);
        }

        public async Task<bool> HasActiveLicenseAsync(int personId, int licenseClassId)
        {
            return await _repository.HasActiveLicenseAsync(personId, licenseClassId);
        }

        public async Task<bool> DeactivateLicenseAsync(int licenseId)
        {
            var existing = await _repository.GetByIdAsync(licenseId);
            if (existing is null)
            {
                throw new KeyNotFoundException($"License with ID {licenseId} was not found.");
            }

            return await _repository.DeactivateLicenseAsync(licenseId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

