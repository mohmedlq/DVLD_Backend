using DVLD.DataAccess.Context;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DVLD.DataAccess.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public PeopleRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(Person entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.People.AddAsync(entity);

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

            return await _context.People
                .Where(x => x.PersonID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.People
                .FirstOrDefaultAsync(x => x.PersonID == id);
        }

        public async Task<Person?> GetByNationalNoAsync(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                return null;
            }

            return await _context.People
                .FirstOrDefaultAsync(x => x.NationalNo == nationalNo);
        }

        public async Task<Person?> GetByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            return await _context.People
                .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<Person?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return await _context.People
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        #endregion

        #region Exists

        public async Task<bool> ExistsByNationalNoAsync(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                return false;
            }

            return await _context.People
                .AnyAsync(x => x.NationalNo == nationalNo);
        }

        public async Task<bool> ExistsByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            return await _context.People
                .AnyAsync(x => x.UserName == userName);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Person entity)
        {
            if (entity is null || entity.PersonID <= 0)
            {
                return false;
            }

            var person = await _context.People
                .FirstOrDefaultAsync(x => x.PersonID == entity.PersonID);

            if (person is null)
            {
                return false;
            }

            person.NationalNo = entity.NationalNo;
            person.FirstName = entity.FirstName;
            person.SecondName = entity.SecondName;
            person.ThirdName = entity.ThirdName;
            person.LastName = entity.LastName;
            person.DateOfBirth = entity.DateOfBirth;
            person.Gender = entity.Gender;
            person.Address = entity.Address;
            person.Phone = entity.Phone;
            person.Email = entity.Email;
            person.CountryID = entity.CountryID;
            person.ImagePath = entity.ImagePath;
            person.UserName = entity.UserName;


            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Password

        public async Task<bool> UpdatePasswordAsync(int id, string password)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var person = await _context.People
                .FirstOrDefaultAsync(x => x.PersonID == id);

            if (person is null)
            {
                return false;
            }

            person.Password = password;

            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> UpdatePasswordAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var person = await _context.People
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (person is null)
            {
                return false;
            }

            person.Password = password;

            return await _context.SaveChangesAsync() > 0;
        }

        #endregion
    }
}