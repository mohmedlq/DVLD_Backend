using DVLD.Application.DTOs.Drivers;
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
    public class DriversController : ControllerBase
    {
        private readonly IDriversService _driversService;

        public DriversController(IDriversService driversService)
        {
            _driversService = driversService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DriverResponseDto>>> GetAllDrivers()
        {
            var drivers = await _driversService.GetAllAsync();
            var response = drivers.Select(d => new DriverResponseDto
            {
                DriverID = d.DriverID,
                PersonID = d.PersonID,
                CreatedByUserID = d.CreatedByUserID,
                CreatedDate = d.CreatedDate
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DriverResponseDto>> GetDriverById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Driver ID must be greater than 0." });
            }

            var driver = await _driversService.GetByIdAsync(id);
            if (driver is null)
            {
                return NotFound(new { Message = $"Driver with ID {id} was not found." });
            }

            var response = new DriverResponseDto
            {
                DriverID = driver.DriverID,
                PersonID = driver.PersonID,
                CreatedByUserID = driver.CreatedByUserID,
                CreatedDate = driver.CreatedDate
            };

            return Ok(response);
        }

        [HttpGet("person/{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DriverResponseDto>> GetDriverByPersonId(int personId)
        {
            if (personId <= 0)
            {
                return BadRequest(new { Message = "Person ID must be greater than 0." });
            }

            var driver = await _driversService.GetByPersonIdAsync(personId);
            if (driver is null)
            {
                return NotFound(new { Message = $"Driver for Person ID {personId} was not found." });
            }

            var response = new DriverResponseDto
            {
                DriverID = driver.DriverID,
                PersonID = driver.PersonID,
                CreatedByUserID = driver.CreatedByUserID,
                CreatedDate = driver.CreatedDate
            };

            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DriverResponseDto>>> GetDriversByUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { Message = "User ID must be greater than 0." });
            }

            var drivers = await _driversService.GetByCreatedByUserIdAsync(userId);
            var response = drivers.Select(d => new DriverResponseDto
            {
                DriverID = d.DriverID,
                PersonID = d.PersonID,
                CreatedByUserID = d.CreatedByUserID,
                CreatedDate = d.CreatedDate
            });

            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _driversService.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DriverResponseDto>> CreateDriver([FromBody] CreateDriverDto dto)
        {
            var driver = new Driver
            {
                PersonID = dto.PersonID,
                CreatedByUserID = dto.CreatedByUserID,
                CreatedDate = System.DateTime.Now
            };

            var isCreated = await _driversService.CreateAsync(driver);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create driver." });
            }

            var response = new DriverResponseDto
            {
                DriverID = driver.DriverID,
                PersonID = driver.PersonID,
                CreatedByUserID = driver.CreatedByUserID,
                CreatedDate = driver.CreatedDate
            };

            return CreatedAtAction(nameof(GetDriverById), new { id = driver.DriverID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDriver(int id, [FromBody] UpdateDriverDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Driver ID must be greater than 0." });
            }

            var existing = await _driversService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Driver with ID {id} was not found." });
            }

            existing.PersonID = dto.PersonID;
            existing.CreatedByUserID = dto.CreatedByUserID;

            var isUpdated = await _driversService.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update driver." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Driver ID must be greater than 0." });
            }

            var existing = await _driversService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Driver with ID {id} was not found." });
            }

            var isDeleted = await _driversService.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Cannot delete this driver because it has associated licenses." });
            }

            return NoContent();
        }
    }
}

