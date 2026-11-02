using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface IOldUserNamesLogsService : ICrud<OldUserNamesLog>
    {
        Task<IEnumerable<OldUserNamesLog>> GetByUsernameAsync(string username);
        Task<int> CountAsync();
    }
}

