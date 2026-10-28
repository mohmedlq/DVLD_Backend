using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Domain.Interfaces
{
    public interface ICrud<T>
    {
        Task<T?>GetByIdAsync(int id);
        Task<IEnumerable<T>>GetAllAsync();
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
