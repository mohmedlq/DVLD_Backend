using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class DetainedLicensesService : IDetainedLicensesService
    {
        private readonly IDetainedLicensesRepository _repository;

        public DetainedLicensesService(IDetainedLicensesRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(DetainedLicense entity)
        {
            if (entity.LicenseID <= 0)
            {
                throw new InvalidOperationException("Invalid License ID.");
            }

            if (entity.CreatedByUserID <= 0)
            {
                throw new InvalidOperationException("Invalid Created By User ID.");
            }

            if (entity.FineFees < 0)
            {
                throw new InvalidOperationException("Fine fees cannot be negative.");
            }

            if (await _repository.IsLicenseDetainedAsync(entity.LicenseID))
            {
                throw new InvalidOperationException("This license is already detained.");
            }

            if (entity.DetainDate == default)
            {
                entity.DetainDate = DateTime.Now;
            }

            entity.IsReleased = false;

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(DetainedLicense entity)
        {
            var existing = await _repository.GetByIdAsync(entity.DetainID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Detained License with ID {entity.DetainID} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Detained License with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DetainedLicense>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DetainedLicense?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region IDetainedLicensesService Implementation

        public async Task<List<DetainedLicense>> GetByLicenseIdAsync(int licenseId)
        {
            return await _repository.GetByLicenseIdAsync(licenseId);
        }

        public async Task<List<DetainedLicense>> GetByCreatedByUserIdAsync(int userId)
        {
            return await _repository.GetByCreatedByUserIdAsync(userId);
        }

        public async Task<List<DetainedLicense>> GetReleasedAsync()
        {
            return await _repository.GetReleasedAsync();
        }

        public async Task<List<DetainedLicense>> GetUnreleasedAsync()
        {
            return await _repository.GetUnreleasedAsync();
        }

        public async Task<bool> ExistsByLicenseIdAsync(int licenseId)
        {
            return await _repository.ExistsByLicenseIdAsync(licenseId);
        }

        public async Task<bool> IsLicenseDetainedAsync(int licenseId)
        {
            return await _repository.IsLicenseDetainedAsync(licenseId);
        }

        public async Task<bool> ReleaseDetainedLicenseAsync(int detainId, int releasedByUserId, int releaseApplicationId)
        {
            var existing = await _repository.GetByIdAsync(detainId);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Detained License with ID {detainId} was not found.");
            }

            if (existing.IsReleased)
            {
                throw new InvalidOperationException("This license is already released.");
            }

            return await _repository.ReleaseDetainedLicenseAsync(detainId, releasedByUserId, releaseApplicationId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

