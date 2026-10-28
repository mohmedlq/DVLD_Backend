using System.Threading.Tasks;
using DVLD.Domain.Entities;

namespace DVLD.Business.Interfaces
{
    public interface IPeopleService : ICrud<Person>
    {

        Task<Person?> GetByNationalNoAsync(string nationalNo);
        Task<Person?> GetByUserNameAsync(string userName);
        Task<Person?> GetByEmailAsync(string email);

        Task<bool> ExistsByNationalNoAsync(string nationalNo);
        Task<bool> ExistsByUserNameAsync(string userName);

        Task<bool> UpdatePasswordAsync(int id, string password);
        Task<bool> UpdatePasswordAsync(string userName, string password);
    }
}