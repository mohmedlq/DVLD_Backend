using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class MassagesService : IMassagesService
    {
        private readonly IMassagesRepository _repository;

        public MassagesService(IMassagesRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(Massage entity)
        {
            if (entity.PersonID <= 0)
            {
                throw new InvalidOperationException("Invalid Person ID.");
            }

            if (string.IsNullOrWhiteSpace(entity.MessageTitle))
            {
                throw new InvalidOperationException("Message title cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(entity.MessageContent))
            {
                throw new InvalidOperationException("Message content cannot be empty.");
            }

            entity.IsRead = false;

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Massage entity)
        {
            var existing = await _repository.GetByIdAsync(entity.MessageID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Message with ID {entity.MessageID} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Message with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Massage>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Massage?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region IMassagesService Implementation

        public async Task<IEnumerable<Massage>> GetByPersonIdAsync(int personId)
        {
            return await _repository.GetByPersonIdAsync(personId);
        }

        public async Task<IEnumerable<Massage>> GetUnreadByPersonIdAsync(int personId)
        {
            return await _repository.GetUnreadByPersonIdAsync(personId);
        }

        public async Task<bool> MarkAsReadAsync(int messageId)
        {
            var existing = await _repository.GetByIdAsync(messageId);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Message with ID {messageId} was not found.");
            }

            return await _repository.MarkAsReadAsync(messageId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

