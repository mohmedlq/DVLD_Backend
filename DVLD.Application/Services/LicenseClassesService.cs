using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class LicenseClassesService : ILicenseClassesService
    {
        private readonly ILicenseClassesRepository _repository;

        public LicenseClassesService(ILicenseClassesRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(LicenseClass entity)
        {
            if (string.IsNullOrWhiteSpace(entity.ClassName))
            {
                throw new InvalidOperationException("License class name cannot be empty.");
            }

            if (entity.ClassFees < 0)
            {
                throw new InvalidOperationException("License class fees cannot be negative.");
            }

            if (await _repository.ClassNameExistsAsync(entity.ClassName))
            {
                throw new InvalidOperationException("A license class with this name already exists.");
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(LicenseClass entity)
        {
            var existing = await _repository.GetByIdAsync(entity.LicenseClassID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"License Class with ID {entity.LicenseClassID} was not found.");
            }

            if (entity.ClassFees < 0)
            {
                throw new InvalidOperationException("License class fees cannot be negative.");
            }

            if (!string.Equals(existing.ClassName, entity.ClassName, StringComparison.OrdinalIgnoreCase))
            {
                if (await _repository.ClassNameExistsAsync(entity.ClassName))
                {
                    throw new InvalidOperationException("A license class with this name already exists.");
                }
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"License Class with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LicenseClass>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<LicenseClass?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<LicenseClass?> GetByClassNameAsync(string className)
        {
            return await _repository.GetByClassNameAsync(className);
        }

        public async Task<bool> ClassNameExistsAsync(string className)
        {
            return await _repository.ClassNameExistsAsync(className);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }
    }
}

