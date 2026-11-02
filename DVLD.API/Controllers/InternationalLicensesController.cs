using DVLD.Application.DTOs.InternationalLicenses;
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
    public class InternationalLicensesController : ControllerBase
    {
        private readonly IInternationalLicensesService _service;

        public InternationalLicensesController(IInternationalLicensesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InternationalLicenseResponseDto>>> GetAll()
        {
            var licenses = await _service.GetAllAsync();
            var response = licenses.Select(l => new InternationalLicenseResponseDto
            {
                InternationalLicenseID = l.InternationalLicenseID,
                ApplicationID = l.ApplicationID,
                DriverID = l.DriverID,
                IssuedUsingLocalLicenseID = l.IssuedUsingLocalLicenseID,
                IssueDate = l.IssueDate,
                ExpirationDate = l.ExpirationDate,
                IsActive = l.IsActive,
                CreatedByUserID = l.CreatedByUserID
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InternationalLicenseResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var l = await _service.GetByIdAsync(id);
            if (l is null)
            {
                return NotFound(new { Message = $"International License with ID {id} was not found." });
            }

            var response = new InternationalLicenseResponseDto
            {
                InternationalLicenseID = l.InternationalLicenseID,
                ApplicationID = l.ApplicationID,
                DriverID = l.DriverID,
                IssuedUsingLocalLicenseID = l.IssuedUsingLocalLicenseID,
                IssueDate = l.IssueDate,
                ExpirationDate = l.ExpirationDate,
                IsActive = l.IsActive,
                CreatedByUserID = l.CreatedByUserID
            };

            return Ok(response);
        }

        [HttpGet("driver/{driverId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InternationalLicenseResponseDto>>> GetByDriverId(int driverId)
        {
            if (driverId <= 0)
            {
                return BadRequest(new { Message = "Driver ID must be greater than 0." });
            }

            var licenses = await _service.GetDriverInternationalLicensesAsync(driverId);
            var response = licenses.Select(l => new InternationalLicenseResponseDto
            {
                InternationalLicenseID = l.InternationalLicenseID,
                ApplicationID = l.ApplicationID,
                DriverID = l.DriverID,
                IssuedUsingLocalLicenseID = l.IssuedUsingLocalLicenseID,
                IssueDate = l.IssueDate,
                ExpirationDate = l.ExpirationDate,
                IsActive = l.IsActive,
                CreatedByUserID = l.CreatedByUserID
            });

            return Ok(response);
        }

        [HttpGet("driver/{driverId}/active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InternationalLicenseResponseDto>> GetActiveByDriverId(int driverId)
        {
            if (driverId <= 0)
            {
                return BadRequest(new { Message = "Driver ID must be greater than 0." });
            }

            var l = await _service.GetActiveByDriverIdAsync(driverId);
            if (l is null)
            {
                return NotFound(new { Message = $"No active international license found for Driver ID {driverId}." });
            }

            var response = new InternationalLicenseResponseDto
            {
                InternationalLicenseID = l.InternationalLicenseID,
                ApplicationID = l.ApplicationID,
                DriverID = l.DriverID,
                IssuedUsingLocalLicenseID = l.IssuedUsingLocalLicenseID,
                IssueDate = l.IssueDate,
                ExpirationDate = l.ExpirationDate,
                IsActive = l.IsActive,
                CreatedByUserID = l.CreatedByUserID
            };

            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _service.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InternationalLicenseResponseDto>> Create([FromBody] CreateInternationalLicenseDto dto)
        {
            var license = new InternationalLicense
            {
                ApplicationID = dto.ApplicationID,
                DriverID = dto.DriverID,
                IssuedUsingLocalLicenseID = dto.IssuedUsingLocalLicenseID,
                IssueDate = System.DateTime.Now,
                ExpirationDate = System.DateTime.Now.AddYears(1),
                IsActive = true,
                CreatedByUserID = dto.CreatedByUserID
            };

            var isCreated = await _service.CreateAsync(license);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to issue international license." });
            }

            var response = new InternationalLicenseResponseDto
            {
                InternationalLicenseID = license.InternationalLicenseID,
                ApplicationID = license.ApplicationID,
                DriverID = license.DriverID,
                IssuedUsingLocalLicenseID = license.IssuedUsingLocalLicenseID,
                IssueDate = license.IssueDate,
                ExpirationDate = license.ExpirationDate,
                IsActive = license.IsActive,
                CreatedByUserID = license.CreatedByUserID
            };

            return CreatedAtAction(nameof(GetById), new { id = license.InternationalLicenseID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInternationalLicenseDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"International License with ID {id} was not found." });
            }

            existing.IsActive = dto.IsActive;
            existing.ExpirationDate = dto.ExpirationDate;

            var isUpdated = await _service.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update international license." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"International License with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete international license." });
            }

            return NoContent();
        }
    }
}

