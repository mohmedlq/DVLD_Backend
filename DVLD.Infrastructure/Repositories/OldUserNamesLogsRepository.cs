using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class OldUserNamesLogsRepository : IOldUserNamesLogsRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public OldUserNamesLogsRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(OldUserNamesLog entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.OldUserNamesLogs.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<OldUserNamesLog>> GetAllAsync()
        {
            return await _context.OldUserNamesLogs
                .AsNoTracking()
                .OrderByDescending(x => x.LogID)
                .ToListAsync();
        }

        public async Task<OldUserNamesLog?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.OldUserNamesLogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LogID == id);
        }

        public async Task<IEnumerable<OldUserNamesLog>> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new List<OldUserNamesLog>();
            }

            return await _context.OldUserNamesLogs
                .AsNoTracking()
                .Where(x => x.OldUserName == username || x.NewUserName == username)
                .OrderByDescending(x => x.LogID)
                .ToListAsync();
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(OldUserNamesLog entity)
        {
            if (entity is null || entity.LogID <= 0)
            {
                return false;
            }

            var log = await _context.OldUserNamesLogs
                .FirstOrDefaultAsync(x => x.LogID == entity.LogID);

            if (log is null)
            {
                return false;
            }

            log.OldUserName = entity.OldUserName;
            log.NewUserName = entity.NewUserName;

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

            return await _context.OldUserNamesLogs
                .Where(x => x.LogID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.OldUserNamesLogs.Select(x => x.LogID).CountAsync();
        }

        #endregion
    }
}

