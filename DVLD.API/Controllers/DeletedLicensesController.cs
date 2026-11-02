using DVLD.Application.DTOs.DeletedLicenses;
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
    public class DeletedLicensesController : ControllerBase
    {
        private readonly IDeletedLicensesService _service;

        public DeletedLicensesController(IDeletedLicensesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DeletedLicenseResponseDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            var response = items.Select(d => new DeletedLicenseResponseDto
            {
                DeletedID = d.DeletedID,
                LicenseID = d.LicenseID,
                ApplicationID = d.ApplicationID,
                DriverID = d.DriverID,
                LicenseClass = d.LicenseClass,
                IssueDate = d.IssueDate,
                ExpirationDate = d.ExpirationDate,
                Notes = d.Notes,
                PaidFees = d.PaidFees,
                IsActive = d.IsActive,
                IssueReason = d.IssueReason,
                CreatedByUserID = d.CreatedByUserID,
                DeletionDate = d.DeletionDate
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeletedLicenseResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var d = await _service.GetByIdAsync(id);
            if (d is null)
            {
                return NotFound(new { Message = $"Deleted license record with ID {id} was not found." });
            }

            var response = new DeletedLicenseResponseDto
            {
                DeletedID = d.DeletedID,
                LicenseID = d.LicenseID,
                ApplicationID = d.ApplicationID,
                DriverID = d.DriverID,
                LicenseClass = d.LicenseClass,
                IssueDate = d.IssueDate,
                ExpirationDate = d.ExpirationDate,
                Notes = d.Notes,
                PaidFees = d.PaidFees,
                IsActive = d.IsActive,
                IssueReason = d.IssueReason,
                CreatedByUserID = d.CreatedByUserID,
                DeletionDate = d.DeletionDate
            };

            return Ok(response);
        }

        [HttpGet("license/{licenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeletedLicenseResponseDto>> GetByLicenseId(int licenseId)
        {
            if (licenseId <= 0)
            {
                return BadRequest(new { Message = "License ID must be greater than 0." });
            }

            var d = await _service.GetByLicenseIdAsync(licenseId);
            if (d is null)
            {
                return NotFound(new { Message = $"Deleted license record for License ID {licenseId} was not found." });
            }

            var response = new DeletedLicenseResponseDto
            {
                DeletedID = d.DeletedID,
                LicenseID = d.LicenseID,
                ApplicationID = d.ApplicationID,
                DriverID = d.DriverID,
                LicenseClass = d.LicenseClass,
                IssueDate = d.IssueDate,
                ExpirationDate = d.ExpirationDate,
                Notes = d.Notes,
                PaidFees = d.PaidFees,
                IsActive = d.IsActive,
                IssueReason = d.IssueReason,
                CreatedByUserID = d.CreatedByUserID,
                DeletionDate = d.DeletionDate
            };

            return Ok(response);
        }

        [HttpGet("driver/{driverId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DeletedLicenseResponseDto>>> GetByDriverId(int driverId)
        {
            if (driverId <= 0)
            {
                return BadRequest(new { Message = "Driver ID must be greater than 0." });
            }

            var items = await _service.GetByDriverIdAsync(driverId);
            var response = items.Select(d => new DeletedLicenseResponseDto
            {
                DeletedID = d.DeletedID,
                LicenseID = d.LicenseID,
                ApplicationID = d.ApplicationID,
                DriverID = d.DriverID,
                LicenseClass = d.LicenseClass,
                IssueDate = d.IssueDate,
                ExpirationDate = d.ExpirationDate,
                Notes = d.Notes,
                PaidFees = d.PaidFees,
                IsActive = d.IsActive,
                IssueReason = d.IssueReason,
                CreatedByUserID = d.CreatedByUserID,
                DeletionDate = d.DeletionDate
            });

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
        public async Task<ActionResult<DeletedLicenseResponseDto>> Create([FromBody] CreateDeletedLicenseDto dto)
        {
            var item = new Deleted_License
            {
                LicenseID = dto.LicenseID,
                ApplicationID = dto.ApplicationID,
                DriverID = dto.DriverID,
                LicenseClass = dto.LicenseClass,
                IssueDate = dto.IssueDate,
                ExpirationDate = dto.ExpirationDate,
                Notes = dto.Notes,
                PaidFees = dto.PaidFees,
                IsActive = dto.IsActive,
                IssueReason = dto.IssueReason,
                CreatedByUserID = dto.CreatedByUserID,
                DeletionDate = System.DateTime.Now
            };

            var isCreated = await _service.CreateAsync(item);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to record deleted license." });
            }

            var response = new DeletedLicenseResponseDto
            {
                DeletedID = item.DeletedID,
                LicenseID = item.LicenseID,
                ApplicationID = item.ApplicationID,
                DriverID = item.DriverID,
                LicenseClass = item.LicenseClass,
                IssueDate = item.IssueDate,
                ExpirationDate = item.ExpirationDate,
                Notes = item.Notes,
                PaidFees = item.PaidFees,
                IsActive = item.IsActive,
                IssueReason = item.IssueReason,
                CreatedByUserID = item.CreatedByUserID,
                DeletionDate = item.DeletionDate
            };

            return CreatedAtAction(nameof(GetById), new { id = item.DeletedID }, response);
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
                return NotFound(new { Message = $"Deleted license record with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete record." });
            }

            return NoContent();
        }
    }
}

