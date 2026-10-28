using DVLD.DataAccess.Context;
using DVLD.Domain;
using DVLD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVLD.DataAccess.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        #region Constructor

        private readonly DvldDbContext _context;

        public CountriesRepository(DvldDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Create

        public async Task<bool> AddAsync(Country entity)
        {
            if (entity is null)
            {
                return false;
            }

            await _context.Countries.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Read

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _context.Countries
                .FirstOrDefaultAsync(x => x.CountryID == id);
        }

        #endregion

        #region Exists

        public async Task<bool> CountryNameExistsAsync(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
            {
                return false;
            }

            return await _context.Countries
                .AnyAsync(x => x.CountryName == countryName);
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Country entity)
        {
            if (entity is null || entity.CountryID <= 0)
            {
                return false;
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(x => x.CountryID == entity.CountryID);

            if (country is null)
            {
                return false;
            }

            country.CountryName = entity.CountryName;

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

            return await _context.Countries
                .Where(x => x.CountryID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #region Count

        public async Task<int> CountAsync()
        {
            return await _context.Countries.Select(C=>C.CountryID).CountAsync();
        }

        #endregion
    }
}