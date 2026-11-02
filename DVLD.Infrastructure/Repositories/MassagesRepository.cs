using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Infrastructure.Repositories
{
    public class MassagesRepository : IMassagesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public MassagesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(Massage entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Massages.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<Massage>> GetAllAsync()
        {
            return await _context.Massages
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Massage?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Massages
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MessageID == id);
        }

        public async Task<IEnumerable<Massage>> GetByPersonIdAsync(int personId)
        {
            if (personId <= 0)
            {
                return new List<Massage>();
            }

            return await _context.Massages
                .AsNoTracking()
                .Where(m => m.PersonID == personId)
                .OrderByDescending(m => m.MessageID)
                .ToListAsync();
        }

        public async Task<IEnumerable<Massage>> GetUnreadByPersonIdAsync(int personId)
        {
            if (personId <= 0)
            {
                return new List<Massage>();
            }

            return await _context.Massages
                .AsNoTracking()
                .Where(m => m.PersonID == personId && !m.IsRead)
                .OrderByDescending(m => m.MessageID)
                .ToListAsync();
        }

        #endregion

        #region MarkRead

        public async Task<bool> MarkAsReadAsync(int messageId)
        {
            if (messageId <= 0)
            {
                return false;
            }

            var msg = await _context.Massages.FirstOrDefaultAsync(m => m.MessageID == messageId);
            if (msg is null)
            {
                return false;
            }

            msg.IsRead = true;
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Massage entity)
        {
            if (entity is null || entity.MessageID <= 0)
            {
                return false;
            }

            var msg = await _context.Massages
                .FirstOrDefaultAsync(m => m.MessageID == entity.MessageID);

            if (msg is null)
            {
                return false;
            }

            msg.PersonID = entity.PersonID;
            msg.MessageTitle = entity.MessageTitle;
            msg.MessageContent = entity.MessageContent;
            msg.IsRead = entity.IsRead;

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

            return await _context.Massages
                .Where(m => m.MessageID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Massages.Select(m => m.MessageID).CountAsync();
        }

        #endregion
    }
}

