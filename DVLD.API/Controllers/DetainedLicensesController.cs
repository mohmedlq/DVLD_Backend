using DVLD.Application.DTOs.DetainedLicenses;
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
    public class DetainedLicensesController : ControllerBase
    {
        private readonly IDetainedLicensesService _service;

        public DetainedLicensesController(IDetainedLicensesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DetainedLicenseResponseDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            var response = items.Select(x => new DetainedLicenseResponseDto
            {
                DetainID = x.DetainID,
                LicenseID = x.LicenseID,
                DetainDate = x.DetainDate,
                FineFees = x.FineFees,
                CreatedByUserID = x.CreatedByUserID,
                IsReleased = x.IsReleased,
                ReleaseDate = x.ReleaseDate,
                ReleasedByUserID = x.ReleasedByUserID,
                ReleaseApplicationID = x.ReleaseApplicationID
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetainedLicenseResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var item = await _service.GetByIdAsync(id);
            if (item is null)
            {
                return NotFound(new { Message = $"Detained license with ID {id} was not found." });
            }

            var response = new DetainedLicenseResponseDto
            {
                DetainID = item.DetainID,
                LicenseID = item.LicenseID,
                DetainDate = item.DetainDate,
                FineFees = item.FineFees,
                CreatedByUserID = item.CreatedByUserID,
                IsReleased = item.IsReleased,
                ReleaseDate = item.ReleaseDate,
                ReleasedByUserID = item.ReleasedByUserID,
                ReleaseApplicationID = item.ReleaseApplicationID
            };

            return Ok(response);
        }

        [HttpGet("license/{licenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DetainedLicenseResponseDto>>> GetByLicenseId(int licenseId)
        {
            if (licenseId <= 0)
            {
                return BadRequest(new { Message = "License ID must be greater than 0." });
            }

            var items = await _service.GetByLicenseIdAsync(licenseId);
            var response = items.Select(x => new DetainedLicenseResponseDto
            {
                DetainID = x.DetainID,
                LicenseID = x.LicenseID,
                DetainDate = x.DetainDate,
                FineFees = x.FineFees,
                CreatedByUserID = x.CreatedByUserID,
                IsReleased = x.IsReleased,
                ReleaseDate = x.ReleaseDate,
                ReleasedByUserID = x.ReleasedByUserID,
                ReleaseApplicationID = x.ReleaseApplicationID
            });

            return Ok(response);
        }

        [HttpGet("is-detained/{licenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> IsLicenseDetained(int licenseId)
        {
            if (licenseId <= 0)
            {
                return BadRequest(new { Message = "License ID must be greater than 0." });
            }

            var isDetained = await _service.IsLicenseDetainedAsync(licenseId);
            return Ok(isDetained);
        }

        [HttpGet("released")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DetainedLicenseResponseDto>>> GetReleased()
        {
            var items = await _service.GetReleasedAsync();
            var response = items.Select(x => new DetainedLicenseResponseDto
            {
                DetainID = x.DetainID,
                LicenseID = x.LicenseID,
                DetainDate = x.DetainDate,
                FineFees = x.FineFees,
                CreatedByUserID = x.CreatedByUserID,
                IsReleased = x.IsReleased,
                ReleaseDate = x.ReleaseDate,
                ReleasedByUserID = x.ReleasedByUserID,
                ReleaseApplicationID = x.ReleaseApplicationID
            });

            return Ok(response);
        }

        [HttpGet("unreleased")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DetainedLicenseResponseDto>>> GetUnreleased()
        {
            var items = await _service.GetUnreleasedAsync();
            var response = items.Select(x => new DetainedLicenseResponseDto
            {
                DetainID = x.DetainID,
                LicenseID = x.LicenseID,
                DetainDate = x.DetainDate,
                FineFees = x.FineFees,
                CreatedByUserID = x.CreatedByUserID,
                IsReleased = x.IsReleased,
                ReleaseDate = x.ReleaseDate,
                ReleasedByUserID = x.ReleasedByUserID,
                ReleaseApplicationID = x.ReleaseApplicationID
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
        public async Task<ActionResult<DetainedLicenseResponseDto>> DetainLicense([FromBody] CreateDetainedLicenseDto dto)
        {
            var detained = new DetainedLicense
            {
                LicenseID = dto.LicenseID,
                FineFees = dto.FineFees,
                CreatedByUserID = dto.CreatedByUserID,
                DetainDate = System.DateTime.Now,
                IsReleased = false
            };

            var isCreated = await _service.CreateAsync(detained);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to detain license." });
            }

            var response = new DetainedLicenseResponseDto
            {
                DetainID = detained.DetainID,
                LicenseID = detained.LicenseID,
                DetainDate = detained.DetainDate,
                FineFees = detained.FineFees,
                CreatedByUserID = detained.CreatedByUserID,
                IsReleased = detained.IsReleased,
                ReleaseDate = detained.ReleaseDate,
                ReleasedByUserID = detained.ReleasedByUserID,
                ReleaseApplicationID = detained.ReleaseApplicationID
            };

            return CreatedAtAction(nameof(GetById), new { id = detained.DetainID }, response);
        }

        [HttpPut("{id}/release")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReleaseLicense(int id, [FromBody] ReleaseDetainedLicenseDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Detained license with ID {id} was not found." });
            }

            var isReleased = await _service.ReleaseDetainedLicenseAsync(id, dto.ReleasedByUserID, dto.ReleaseApplicationID);
            if (!isReleased)
            {
                return BadRequest(new { Message = "Failed to release detained license." });
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDetainedLicenseDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Detained license with ID {id} was not found." });
            }

            existing.FineFees = dto.FineFees;
            existing.CreatedByUserID = dto.CreatedByUserID;

            var isUpdated = await _service.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update detained license." });
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
                return NotFound(new { Message = $"Detained license with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete detained license." });
            }

            return NoContent();
        }
    }
}

