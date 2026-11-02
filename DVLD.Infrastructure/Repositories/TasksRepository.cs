using TaskEntity = DVLD.Domain.Entities.Tasks;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public TasksRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(TaskEntity entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Tasks.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TaskID == id);
        }

        public async Task<IEnumerable<TaskEntity>> GetByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                return new List<TaskEntity>();
            }

            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.UserID == userId)
                .OrderByDescending(t => t.TaskID)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetPendingTasksAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => !t.Status)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        #endregion

        #region Complete

        public async Task<bool> CompleteTaskAsync(int taskId)
        {
            if (taskId <= 0)
            {
                return false;
            }

            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskID == taskId);
            if (task is null)
            {
                return false;
            }

            task.Status = true;
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(TaskEntity entity)
        {
            if (entity is null || entity.TaskID <= 0)
            {
                return false;
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.TaskID == entity.TaskID);

            if (task is null)
            {
                return false;
            }

            task.UserID = entity.UserID;
            task.TaskTitle = entity.TaskTitle;
            task.Description = entity.Description;
            task.AssignedDate = entity.AssignedDate;
            task.DueDate = entity.DueDate;
            task.Status = entity.Status;

            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return await _context.Tasks
                .Where(t => t.TaskID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Tasks.Select(t => t.TaskID).CountAsync();
        }

        #endregion
    }
}

