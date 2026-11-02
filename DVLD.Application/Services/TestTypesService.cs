using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD.Application.Services
{
    public class TestTypesService : ITestTypesService
    {
        private readonly ITestTypesRepository _testTypesRepository;

        public TestTypesService(ITestTypesRepository testTypesRepository)
        {
            _testTypesRepository = testTypesRepository;
        }

        #region ICrud Implementation

        public async Task<bool> CreateAsync(TestType entity)
        {
            if (string.IsNullOrWhiteSpace(entity.TestTypeTitle))
            {
                throw new InvalidOperationException("Test type title cannot be empty.");
            }

            if (entity.TestTypeFees < 0)
            {
                throw new InvalidOperationException("Test type fees cannot be negative.");
            }

            if (await _testTypesRepository.TestTypeTitleExistsAsync(entity.TestTypeTitle))
            {
                throw new InvalidOperationException("A test type with this title already exists.");
            }

            return await _testTypesRepository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(TestType entity)
        {
            var existing = await _testTypesRepository.GetByIdAsync(entity.TestTypeID);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Test Type with ID {entity.TestTypeID} was not found.");
            }

            if (entity.TestTypeFees < 0)
            {
                throw new InvalidOperationException("Test type fees cannot be negative.");
            }

            if (!string.Equals(existing.TestTypeTitle, entity.TestTypeTitle, StringComparison.OrdinalIgnoreCase))
            {
                if (await _testTypesRepository.TestTypeTitleExistsAsync(entity.TestTypeTitle))
                {
                    throw new InvalidOperationException("A test type with this title already exists.");
                }
            }

            return await _testTypesRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _testTypesRepository.GetByIdAsync(id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Test Type with ID {id} was not found.");
            }

            return await _testTypesRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TestType>> GetAllAsync()
        {
            return await _testTypesRepository.GetAllAsync();
        }

        public async Task<TestType?> GetByIdAsync(int id)
        {
            return await _testTypesRepository.GetByIdAsync(id);
        }

        #endregion

        #region ITestTypesService Implementation

        public async Task<decimal?> GetTestTypeFeesAsync(int testTypeId)
        {
            return await _testTypesRepository.GetTestTypeFeesAsync(testTypeId);
        }

        public async Task<bool> TestTypeTitleExistsAsync(string testTypeTitle)
        {
            return await _testTypesRepository.TestTypeTitleExistsAsync(testTypeTitle);
        }

        public async Task<int> CountAsync()
        {
            return await _testTypesRepository.CountAsync();
        }

        #endregion
    }
}

