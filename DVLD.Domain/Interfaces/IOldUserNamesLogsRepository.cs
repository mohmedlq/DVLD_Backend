using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface IOldUserNamesLogsRepository : ICrud<OldUserNamesLog>
    {
        Task<IEnumerable<OldUserNamesLog>> GetByUsernameAsync(string username);
        Task<int> CountAsync();
    }
}

