using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public LogsRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(Log entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Logs.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            return await _context.Logs
                .AsNoTracking()
                .OrderByDescending(l => l.LogId)
                .ToListAsync();
        }

        public async Task<Log?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Logs
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.LogId == id);
        }

        public async Task<IEnumerable<Log>> GetByLevelAsync(string level)
        {
            if (string.IsNullOrWhiteSpace(level))
            {
                return new List<Log>();
            }

            return await _context.Logs
                .AsNoTracking()
                .Where(l => l.Level == level)
                .OrderByDescending(l => l.LogId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetRecentLogsAsync(int count)
        {
            if (count <= 0) count = 50;

            return await _context.Logs
                .AsNoTracking()
                .OrderByDescending(l => l.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Log entity)
        {
            if (entity is null || entity.LogId <= 0)
            {
                return false;
            }

            var log = await _context.Logs
                .FirstOrDefaultAsync(l => l.LogId == entity.LogId);

            if (log is null)
            {
                return false;
            }

            log.Level = entity.Level;
            log.Message = entity.Message;
            log.ExceptionType = entity.ExceptionType;
            log.StackTrace = entity.StackTrace;
            log.RequestPath = entity.RequestPath;
            log.HttpMethod = entity.HttpMethod;
            log.UserId = entity.UserId;
            log.CreatedAt = entity.CreatedAt;

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

            return await _context.Logs
                .Where(l => l.LogId == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Logs.Select(l => l.LogId).CountAsync();
        }

        #endregion
    }
}

