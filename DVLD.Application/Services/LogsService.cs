using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class LogsService : ILogsService
    {
        private readonly ILogsRepository _repository;

        public LogsService(ILogsRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(Log entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Level))
            {
                throw new InvalidOperationException("Log level is required.");
            }

            if (string.IsNullOrWhiteSpace(entity.Message))
            {
                throw new InvalidOperationException("Log message is required.");
            }

            if (entity.CreatedAt == default)
            {
                entity.CreatedAt = DateTime.Now;
            }

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Log entity)
        {
            var existing = await _repository.GetByIdAsync(entity.LogId);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Log with ID {entity.LogId} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Log with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Log?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region ILogsService Implementation

        public async Task<IEnumerable<Log>> GetByLevelAsync(string level)
        {
            return await _repository.GetByLevelAsync(level);
        }

        public async Task<IEnumerable<Log>> GetRecentLogsAsync(int count)
        {
            return await _repository.GetRecentLogsAsync(count);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

