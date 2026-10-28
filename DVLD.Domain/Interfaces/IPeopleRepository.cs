using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD.Domain.Entities;

namespace DVLD.Domain.Interfaces
{    
 public interface IPeopleRepository : ICrud<Person>
 {
     Task<Person?> GetByNationalNoAsync(string nationalNo);

     Task<Person?> GetByEmailAsync(string email);

     Task<Person?> GetByUserNameAsync(string userName);

     Task<bool> ExistsByNationalNoAsync(string nationalNo);

     Task<bool> ExistsByUserNameAsync(string userName);
      Task<bool> UpdatePasswordAsync(int id,string password);
       Task<bool> UpdatePasswordAsync(string UserName,string password);


    }
}

