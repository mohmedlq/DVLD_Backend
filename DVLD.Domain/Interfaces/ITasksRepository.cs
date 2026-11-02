using TaskEntity = DVLD.Domain.Entities.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface ITasksRepository : ICrud<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetByUserIdAsync(int userId);
        Task<IEnumerable<TaskEntity>> GetPendingTasksAsync();
        Task<bool> CompleteTaskAsync(int taskId);
        Task<int> CountAsync();
    }
}

