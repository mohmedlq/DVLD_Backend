using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface IMassagesRepository : ICrud<Massage>
    {
        Task<IEnumerable<Massage>> GetByPersonIdAsync(int personId);
        Task<IEnumerable<Massage>> GetUnreadByPersonIdAsync(int personId);
        Task<bool> MarkAsReadAsync(int messageId);
        Task<int> CountAsync();
    }
}

