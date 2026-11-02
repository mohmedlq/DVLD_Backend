using DVLD.Application.DTOs.Countries;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CountryResponseDto>>> GetAllCountries()
        {
            var countries = await _countriesService.GetAllAsync();
            var response = countries.Select(c => new CountryResponseDto
            {
                CountryID = c.CountryID,
                CountryName = c.CountryName
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryResponseDto>> GetCountryById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var country = await _countriesService.GetByIdAsync(id);
            if (country is null)
            {
                return NotFound(new { Message = $"Country with ID {id} was not found." });
            }

            var response = new CountryResponseDto
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };

            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _countriesService.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryResponseDto>> CreateCountry([FromBody] CreateCountryDto dto)
        {
            var country = new Country
            {
                CountryName = dto.CountryName
            };

            var isCreated = await _countriesService.CreateAsync(country);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create the country." });
            }

            var response = new CountryResponseDto
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };

            return CreatedAtAction(nameof(GetCountryById), new { id = country.CountryID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _countriesService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Country with ID {id} was not found." });
            }

            existing.CountryName = dto.CountryName;

            var isUpdated = await _countriesService.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update country." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _countriesService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Country with ID {id} was not found." });
            }

            var isDeleted = await _countriesService.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Cannot delete this country because it is referenced by other records." });
            }

            return NoContent();
        }
    }
}

