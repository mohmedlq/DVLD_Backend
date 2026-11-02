using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public async Task<bool> CreateAsync(Country entity)
        {
            if (string.IsNullOrWhiteSpace(entity.CountryName))
            {
                throw new InvalidOperationException("Country name cannot be empty.");
            }

            if (await _countriesRepository.CountryNameExistsAsync(entity.CountryName))
            {
                throw new InvalidOperationException("A country with this name already exists.");
            }

            return await _countriesRepository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(Country entity)
        {
            var existing = await _countriesRepository.GetByIdAsync(entity.CountryID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Country with ID {entity.CountryID} was not found.");
            }

            if (!string.Equals(existing.CountryName, entity.CountryName, StringComparison.OrdinalIgnoreCase))
            {
                if (await _countriesRepository.CountryNameExistsAsync(entity.CountryName))
                {
                    throw new InvalidOperationException("A country with this name already exists.");
                }
            }

            return await _countriesRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _countriesRepository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Country with ID {id} was not found.");
            }

            return await _countriesRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _countriesRepository.GetAllAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _countriesRepository.GetByIdAsync(id);
        }

        public async Task<bool> CountryNameExistsAsync(string countryName)
        {
            return await _countriesRepository.CountryNameExistsAsync(countryName);
        }

        public async Task<int> CountAsync()
        {
            return await _countriesRepository.CountAsync();
        }
    }
}

