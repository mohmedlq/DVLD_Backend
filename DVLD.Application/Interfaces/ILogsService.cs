using DVLD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Interfaces
{
    public interface ILogsService : ICrud<Log>
    {
        Task<IEnumerable<Log>> GetByLevelAsync(string level);
        Task<IEnumerable<Log>> GetRecentLogsAsync(int count);
        Task<int> CountAsync();
    }
}

