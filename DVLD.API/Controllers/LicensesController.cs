using DVLD.Application.DTOs.Licenses;
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
    public class LicensesController : ControllerBase
    {
        private readonly ILicensesService _licensesService;

        public LicensesController(ILicensesService licensesService)
        {
            _licensesService = licensesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LicenseResponseDto>>> GetAllLicenses()
        {
            var licenses = await _licensesService.GetAllAsync();
            var response = licenses.Select(l => new LicenseResponseDto
            {
                LicenseID = l.LicenseID,
                ApplicationID = l.ApplicationID,
                DriverID = l.DriverID,
                LicenseClass = l.LicenseClass,
                IssueDate = l.IssueDate,
                ExpirationDate = l.ExpirationDate,
                Notes = l.Notes,
                PaidFees = l.PaidFees,
                IsActive = l.IsActive,
                IssueReason = l.IssueReason,
                CreatedByUserID = l.CreatedByUserID,
                IsDeleted = l.IsDeleted
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LicenseResponseDto>> GetLicenseById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "License ID must be greater than 0." });
            }

            var l = await _licensesService.GetByIdAsync(id);
            if (l is null)
            {
                return NotFound(new { Message = $"License with ID {id} was not found." });
            }

            var response = new LicenseResponseDto
            {
                LicenseID = l.LicenseID,
                ApplicationID = l.ApplicationID,
                DriverID = l.DriverID,
                LicenseClass = l.LicenseClass,
                IssueDate = l.IssueDate,
                ExpirationDate = l.ExpirationDate,
                Notes = l.Notes,
                PaidFees = l.PaidFees,
                IsActive = l.IsActive,
                IssueReason = l.IssueReason,
                CreatedByUserID = l.CreatedByUserID,
                IsDeleted = l.IsDeleted
            };

            return Ok(response);
        }

        [HttpGet("driver/{driverId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LicenseResponseDto>>> GetDriverLicenses(int driverId)
        {
            if (driverId <= 0)
            {
                return BadRequest(new { Message = "Driver ID must be greater than 0." });
            }

            var licenses = await _licensesService.GetDriverLicensesAsync(driverId);
            var response = licenses.Select(l => new LicenseResponseDto
            {
                LicenseID = l.LicenseID,
                ApplicationID = l.ApplicationID,
                DriverID = l.DriverID,
                LicenseClass = l.LicenseClass,
                IssueDate = l.IssueDate,
                ExpirationDate = l.ExpirationDate,
                Notes = l.Notes,
                PaidFees = l.PaidFees,
                IsActive = l.IsActive,
                IssueReason = l.IssueReason,
                CreatedByUserID = l.CreatedByUserID,
                IsDeleted = l.IsDeleted
            });

            return Ok(response);
        }

        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LicenseResponseDto>> GetActiveLicenseByPersonAndClass([FromQuery] int personId, [FromQuery] int licenseClassId)
        {
            if (personId <= 0 || licenseClassId <= 0)
            {
                return BadRequest(new { Message = "Person ID and License Class ID must be greater than 0." });
            }

            var l = await _licensesService.GetActiveLicenseByPersonIdAsync(personId, licenseClassId);
            if (l is null)
            {
                return NotFound(new { Message = $"No active license found for Person ID {personId} and License Class {licenseClassId}." });
            }

            var response = new LicenseResponseDto
            {
                LicenseID = l.LicenseID,
                ApplicationID = l.ApplicationID,
                DriverID = l.DriverID,
                LicenseClass = l.LicenseClass,
                IssueDate = l.IssueDate,
                ExpirationDate = l.ExpirationDate,
                Notes = l.Notes,
                PaidFees = l.PaidFees,
                IsActive = l.IsActive,
                IssueReason = l.IssueReason,
                CreatedByUserID = l.CreatedByUserID,
                IsDeleted = l.IsDeleted
            };

            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _licensesService.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LicenseResponseDto>> CreateLicense([FromBody] CreateLicenseDto dto)
        {
            var license = new License
            {
                ApplicationID = dto.ApplicationID,
                DriverID = dto.DriverID,
                LicenseClass = dto.LicenseClass,
                IssueDate = System.DateTime.Now,
                ExpirationDate = dto.ExpirationDate,
                Notes = dto.Notes,
                PaidFees = dto.PaidFees,
                IsActive = true,
                IssueReason = dto.IssueReason,
                CreatedByUserID = dto.CreatedByUserID,
                IsDeleted = false
            };

            var isCreated = await _licensesService.CreateAsync(license);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to issue license." });
            }

            var response = new LicenseResponseDto
            {
                LicenseID = license.LicenseID,
                ApplicationID = license.ApplicationID,
                DriverID = license.DriverID,
                LicenseClass = license.LicenseClass,
                IssueDate = license.IssueDate,
                ExpirationDate = license.ExpirationDate,
                Notes = license.Notes,
                PaidFees = license.PaidFees,
                IsActive = license.IsActive,
                IssueReason = license.IssueReason,
                CreatedByUserID = license.CreatedByUserID,
                IsDeleted = license.IsDeleted
            };

            return CreatedAtAction(nameof(GetLicenseById), new { id = license.LicenseID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLicense(int id, [FromBody] UpdateLicenseDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "License ID must be greater than 0." });
            }

            var existing = await _licensesService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"License with ID {id} was not found." });
            }

            existing.ExpirationDate = dto.ExpirationDate;
            existing.Notes = dto.Notes;
            existing.PaidFees = dto.PaidFees;
            existing.IsActive = dto.IsActive;
            existing.IssueReason = dto.IssueReason;
            existing.CreatedByUserID = dto.CreatedByUserID;

            var isUpdated = await _licensesService.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update license." });
            }

            return NoContent();
        }

        [HttpPut("{id}/deactivate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeactivateLicense(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "License ID must be greater than 0." });
            }

            var existing = await _licensesService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"License with ID {id} was not found." });
            }

            var isDeactivated = await _licensesService.DeactivateLicenseAsync(id);
            if (!isDeactivated)
            {
                return BadRequest(new { Message = "Failed to deactivate license." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLicense(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "License ID must be greater than 0." });
            }

            var existing = await _licensesService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"License with ID {id} was not found." });
            }

            var isDeleted = await _licensesService.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete license." });
            }

            return NoContent();
        }
    }
}

