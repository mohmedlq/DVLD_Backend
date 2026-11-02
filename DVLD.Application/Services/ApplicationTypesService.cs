using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Enums;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class ApplicationTypesService : IApplicationTypesService
    {
        private readonly IApplicationTypesRepository _repository;

        public ApplicationTypesService(IApplicationTypesRepository repository)
        {
            _repository = repository;
        }

        #region Private Helpers

        private static int GetValidatedId(enApplicationType id)
        {
            if (!Enum.IsDefined(typeof(enApplicationType), id))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(id),
                    id,
                    "Invalid application type ID.");
            }

            return (int)id;
        }

        #endregion

        #region CRUD Implementation

        public async Task<bool> CreateAsync(ApplicationType entity)
        {
            if (string.IsNullOrWhiteSpace(entity.ApplicationTypeTitle))
            {
                throw new InvalidOperationException(
                    "Application type title cannot be empty.");
            }

            if (entity.ApplicationFees < 0)
            {
                throw new InvalidOperationException(
                    "Application fees cannot be negative.");
            }

            if (await _repository.ApplicationTypeTitleExistsAsync(
                entity.ApplicationTypeTitle))
            {
                throw new InvalidOperationException(
                    "An application type with this title already exists.");
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(ApplicationType entity)
        {
            var existing = await _repository.GetByIdAsync(
                entity.ApplicationTypeID);

            if (existing is null)
            {
                throw new KeyNotFoundException(
                    $"Application Type with ID {entity.ApplicationTypeID} was not found.");
            }

            if (string.IsNullOrWhiteSpace(entity.ApplicationTypeTitle))
            {
                throw new InvalidOperationException(
                    "Application type title cannot be empty.");
            }

            if (entity.ApplicationFees < 0)
            {
                throw new InvalidOperationException(
                    "Application fees cannot be negative.");
            }

            if (!string.Equals(
                existing.ApplicationTypeTitle,
                entity.ApplicationTypeTitle,
                StringComparison.OrdinalIgnoreCase))
            {
                if (await _repository.ApplicationTypeTitleExistsAsync(
                    entity.ApplicationTypeTitle))
                {
                    throw new InvalidOperationException(
                        "An application type with this title already exists.");
                }
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(enApplicationType id)
        {
            int applicationTypeId = GetValidatedId(id);

            var existing = await _repository.GetByIdAsync(applicationTypeId);

            if (existing is null)
            {
                throw new KeyNotFoundException(
                    $"Application Type with ID {applicationTypeId} was not found.");
            }

            return await _repository.DeleteAsync(applicationTypeId);
        }

        public async Task<IEnumerable<ApplicationType>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ApplicationType?> GetByIdAsync(enApplicationType id)
        {
            int applicationTypeId = GetValidatedId(id);

            return await _repository.GetByIdAsync(applicationTypeId);
        }

        #endregion

        #region Application Types Specific Methods

        public async Task<decimal?> GetApplicationFeesAsync(
            enApplicationType applicationTypeId)
        {
            int id = GetValidatedId(applicationTypeId);

            return await _repository.GetApplicationFees(id);
        }

        public async Task<bool> ApplicationTypeTitleExistsAsync(
            string applicationTypeTitle)
        {
            return await _repository.ApplicationTypeTitleExistsAsync(
                applicationTypeTitle);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}