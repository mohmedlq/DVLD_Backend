using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class OldUserNamesLogsService : IOldUserNamesLogsService
    {
        private readonly IOldUserNamesLogsRepository _repository;

        public OldUserNamesLogsService(IOldUserNamesLogsRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(OldUserNamesLog entity)
        {
            if (string.IsNullOrWhiteSpace(entity.OldUserName) && string.IsNullOrWhiteSpace(entity.NewUserName))
            {
                throw new InvalidOperationException("At least one of old username or new username must be provided.");
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(OldUserNamesLog entity)
        {
            var existing = await _repository.GetByIdAsync(entity.LogID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Old username log with ID {entity.LogID} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Old username log with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OldUserNamesLog>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OldUserNamesLog?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region IOldUserNamesLogsService Implementation

        public async Task<IEnumerable<OldUserNamesLog>> GetByUsernameAsync(string username)
        {
            return await _repository.GetByUsernameAsync(username);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

