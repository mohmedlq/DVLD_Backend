using DVLD.Business.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Business.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task<bool> CreateAsync(Person entity)
        {
            if (entity.DateOfBirth > DateTime.Now)
            {
                throw new InvalidOperationException("Date of birth cannot be in the future.");
            }

            if (await _peopleRepository.ExistsByNationalNoAsync(entity.NationalNo))
            {
                throw new InvalidOperationException("The National Number is already registered in the system.");
            }

            if (await _peopleRepository.ExistsByUserNameAsync(entity.UserName))
            {
                throw new InvalidOperationException("The Username is already taken by another person.");
            }

            return await _peopleRepository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Person entity)
        {
            var existingPerson = await _peopleRepository.GetByIdAsync(entity.PersonID);

            if (existingPerson == null)
            {
                throw new KeyNotFoundException("Person not found.");
            }

            if (!string.Equals(existingPerson.NationalNo, entity.NationalNo, StringComparison.OrdinalIgnoreCase))
            {
                if (await _peopleRepository.ExistsByNationalNoAsync(entity.NationalNo))
                {
                    throw new InvalidOperationException("The new National Number is already registered to another person.");
                }
            }

            if (!string.Equals(existingPerson.UserName, entity.UserName, StringComparison.OrdinalIgnoreCase))
            {
                if (await _peopleRepository.ExistsByUserNameAsync(entity.UserName))
                {
                    throw new InvalidOperationException("The new Username is already taken.");
                }
            }

            return await _peopleRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
          
            return await _peopleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _peopleRepository.GetAllAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _peopleRepository.GetByIdAsync(id);
        }

        public async Task<Person?> GetByNationalNoAsync(string nationalNo)
        {
            return await _peopleRepository.GetByNationalNoAsync(nationalNo);
        }

        public async Task<Person?> GetByUserNameAsync(string userName)
        {
            return await _peopleRepository.GetByUserNameAsync(userName);
        }

        public async Task<Person?> GetByEmailAsync(string email)
        {
            return await _peopleRepository.GetByEmailAsync(email);
        }

        public async Task<bool> ExistsByNationalNoAsync(string nationalNo)
        {
            return await _peopleRepository.ExistsByNationalNoAsync(nationalNo);
        }

        public async Task<bool> ExistsByUserNameAsync(string userName)
        {
            return await _peopleRepository.ExistsByUserNameAsync(userName);
        }

        public async Task<bool> UpdatePasswordAsync(int id, string password)
        {
            return await _peopleRepository.UpdatePasswordAsync(id, password);
        }

        public async Task<bool> UpdatePasswordAsync(string userName, string password)
        {
            return await _peopleRepository.UpdatePasswordAsync(userName, password);
        }
    }
}