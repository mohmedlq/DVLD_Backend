using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class InternationalLicensesService : IInternationalLicensesService
    {
        private readonly IInternationalLicensesRepository _repository;

        public InternationalLicensesService(IInternationalLicensesRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(InternationalLicense entity)
        {
            if (entity.ApplicationID <= 0)
            {
                throw new InvalidOperationException("Invalid Application ID.");
            }

            if (entity.DriverID <= 0)
            {
                throw new InvalidOperationException("Invalid Driver ID.");
            }

            if (entity.IssuedUsingLocalLicenseID <= 0)
            {
                throw new InvalidOperationException("Invalid Local License ID.");
            }

            if (entity.CreatedByUserID <= 0)
            {
                throw new InvalidOperationException("Invalid Created By User ID.");
            }

            if (await _repository.HasActiveInternationalLicenseAsync(entity.DriverID))
            {
                throw new InvalidOperationException("This driver already has an active international license.");
            }

            if (entity.IssueDate == default)
            {
                entity.IssueDate = DateTime.Now;
            }

            if (entity.ExpirationDate == default)
            {
                entity.ExpirationDate = entity.IssueDate.AddYears(1);
            }

            entity.IsActive = true;

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(InternationalLicense entity)
        {
            var existing = await _repository.GetByIdAsync(entity.InternationalLicenseID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"International License with ID {entity.InternationalLicenseID} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"International License with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<InternationalLicense>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<InternationalLicense?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region IInternationalLicensesService Implementation

        public async Task<IEnumerable<InternationalLicense>> GetDriverInternationalLicensesAsync(int driverId)
        {
            return await _repository.GetDriverInternationalLicensesAsync(driverId);
        }

        public async Task<InternationalLicense?> GetActiveByDriverIdAsync(int driverId)
        {
            return await _repository.GetActiveByDriverIdAsync(driverId);
        }

        public async Task<bool> HasActiveInternationalLicenseAsync(int driverId)
        {
            return await _repository.HasActiveInternationalLicenseAsync(driverId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

