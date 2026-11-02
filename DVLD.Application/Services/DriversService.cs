using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class DriversService : IDriversService
    {
        private readonly IDriversRepository _driversRepository;

        public DriversService(IDriversRepository driversRepository)
        {
            _driversRepository = driversRepository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(Driver entity)
        {
            if (entity.PersonID <= 0)
            {
                throw new InvalidOperationException("Invalid Person ID.");
            }

            if (await _driversRepository.ExistsByPersonIdAsync(entity.PersonID))
            {
                throw new InvalidOperationException("A driver already exists for this person.");
            }

            if (entity.CreatedDate == default)
            {
                entity.CreatedDate = DateTime.Now;
            }

            return await _driversRepository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Driver entity)
        {
            var existing = await _driversRepository.GetByIdAsync(entity.DriverID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Driver with ID {entity.DriverID} was not found.");
            }

            if (existing.PersonID != entity.PersonID)
            {
                if (await _driversRepository.ExistsByPersonIdAsync(entity.PersonID))
                {
                    throw new InvalidOperationException("A driver already exists for this person.");
                }
            }

            return await _driversRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _driversRepository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Driver with ID {id} was not found.");
            }

            return await _driversRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _driversRepository.GetAllAsync();
        }

        public async Task<Driver?> GetByIdAsync(int id)
        {
            return await _driversRepository.GetByIdAsync(id);
        }

        #endregion

        #region IDriversService Implementation

        public async Task<Driver?> GetByPersonIdAsync(int personId)
        {
            return await _driversRepository.GetByPersonIdAsync(personId);
        }

        public async Task<bool> ExistsByPersonIdAsync(int personId)
        {
            return await _driversRepository.ExistsByPersonIdAsync(personId);
        }

        public async Task<int> CountAsync()
        {
            return await _driversRepository.CountAsync();
        }

        public async Task<List<Driver>> GetByCreatedByUserIdAsync(int userId)
        {
            return await _driversRepository.GetByCreatedByUserIdAsync(userId);
        }

        #endregion
    }
}

