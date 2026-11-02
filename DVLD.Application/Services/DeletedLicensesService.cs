using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class DeletedLicensesService : IDeletedLicensesService
    {
        private readonly IDeletedLicensesRepository _repository;

        public DeletedLicensesService(IDeletedLicensesRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(Deleted_License entity)
        {
            if (entity.LicenseID <= 0)
            {
                throw new InvalidOperationException("Invalid License ID.");
            }

            if (entity.DeletionDate == null)
            {
                entity.DeletionDate = DateTime.Now;
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Deleted_License entity)
        {
            var existing = await _repository.GetByIdAsync(entity.DeletedID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Deleted license log with ID {entity.DeletedID} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Deleted license log with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Deleted_License>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Deleted_License?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region IDeletedLicensesService Implementation

        public async Task<Deleted_License?> GetByLicenseIdAsync(int licenseId)
        {
            return await _repository.GetByLicenseIdAsync(licenseId);
        }

        public async Task<IEnumerable<Deleted_License>> GetByDriverIdAsync(int driverId)
        {
            return await _repository.GetByDriverIdAsync(driverId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

