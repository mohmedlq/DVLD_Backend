using TaskEntity = DVLD.Domain.Entities.Tasks;
using DVLD.Application.Interfaces;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _repository;

        public TasksService(ITasksRepository repository)
        {
            _repository = repository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(TaskEntity entity)
        {
            if (entity.UserID <= 0)
            {
                throw new InvalidOperationException("Invalid User ID.");
            }

            if (string.IsNullOrWhiteSpace(entity.TaskTitle))
            {
                throw new InvalidOperationException("Task title cannot be empty.");
            }

            if (entity.AssignedDate == default)
            {
                entity.AssignedDate = DateOnly.FromDateTime(DateTime.Now);
            }

            if (entity.DueDate != default && entity.DueDate < entity.AssignedDate)
            {
                throw new InvalidOperationException("Due date cannot be before assigned date.");
            }

            entity.Status = false;

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(TaskEntity entity)
        {
            var existing = await _repository.GetByIdAsync(entity.TaskID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Task with ID {entity.TaskID} was not found.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Task with ID {id} was not found.");
            }

            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        #endregion

        #region ITasksService Implementation

        public async Task<IEnumerable<TaskEntity>> GetByUserIdAsync(int userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<TaskEntity>> GetPendingTasksAsync()
        {
            return await _repository.GetPendingTasksAsync();
        }

        public async Task<bool> CompleteTaskAsync(int taskId)
        {
            var existing = await _repository.GetByIdAsync(taskId);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Task with ID {taskId} was not found.");
            }

            return await _repository.CompleteTaskAsync(taskId);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion
    }
}

