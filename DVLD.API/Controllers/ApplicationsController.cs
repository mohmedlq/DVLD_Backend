using DVLD.Application.DTOs.Applications;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using AppEntity = DVLD.Domain.Entities.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DVLD.Domain.Enums;

namespace DVLD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationsService _applicationsService;

        public ApplicationsController(IApplicationsService applicationsService)
        {
            _applicationsService = applicationsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ApplicationResponseDto>>> GetAllApplications()
        {
            var apps = await _applicationsService.GetAllAsync();
            var response = apps.Select(a => new ApplicationResponseDto
            {
                ApplicationID = a.ApplicationID,
                ApplicantPersonID = a.ApplicantPersonID,
                ApplicationDate = a.ApplicationDate,
                ApplicationTypeID = a.ApplicationTypeID,
                ApplicationStatus = a.ApplicationStatus,
                LastStatusDate = a.LastStatusDate,
                PaidFees = a.PaidFees,
                CreatedByUserID = a.CreatedByUserID
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationResponseDto>> GetApplicationById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Application ID must be greater than 0." });
            }

            var app = await _applicationsService.GetByIdAsync(id);
            if (app is null)
            {
                return NotFound(new { Message = $"Application with ID {id} was not found." });
            }

            var response = new ApplicationResponseDto
            {
                ApplicationID = app.ApplicationID,
                ApplicantPersonID = app.ApplicantPersonID,
                ApplicationDate = app.ApplicationDate,
                ApplicationTypeID = app.ApplicationTypeID,
                ApplicationStatus = app.ApplicationStatus,
                LastStatusDate = app.LastStatusDate,
                PaidFees = app.PaidFees,
                CreatedByUserID = app.CreatedByUserID
            };

            return Ok(response);
        }

        [HttpGet("person/{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationResponseDto>> GetByPersonId(int personId)
        {
            if (personId <= 0)
            {
                return BadRequest(new { Message = "Person ID must be greater than 0." });
            }

            var app = await _applicationsService.GetByApplicantPersonIdAsync(personId);
            if (app is null)
            {
                return NotFound(new { Message = $"Application for Person ID {personId} was not found." });
            }

            var response = new ApplicationResponseDto
            {
                ApplicationID = app.ApplicationID,
                ApplicantPersonID = app.ApplicantPersonID,
                ApplicationDate = app.ApplicationDate,
                ApplicationTypeID = app.ApplicationTypeID,
                ApplicationStatus = app.ApplicationStatus,
                LastStatusDate = app.LastStatusDate,
                PaidFees = app.PaidFees,
                CreatedByUserID = app.CreatedByUserID
            };

            return Ok(response);
        }

        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ApplicationResponseDto>>> GetByStatus(enApplicationStatus status)
        {
            var apps = await _applicationsService.GetByStatusAsync(status);
            var response = apps.Select(a => new ApplicationResponseDto
            {
                ApplicationID = a.ApplicationID,
                ApplicantPersonID = a.ApplicantPersonID,
                ApplicationDate = a.ApplicationDate,
                ApplicationTypeID = a.ApplicationTypeID,
                ApplicationStatus = a.ApplicationStatus,
                LastStatusDate = a.LastStatusDate,
                PaidFees = a.PaidFees,
                CreatedByUserID = a.CreatedByUserID
            });

            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ApplicationResponseDto>>> GetByUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { Message = "User ID must be greater than 0." });
            }

            var apps = await _applicationsService.AllApplicationsForUserId(userId);
            var response = apps.Select(a => new ApplicationResponseDto
            {
                ApplicationID = a.ApplicationID,
                ApplicantPersonID = a.ApplicantPersonID,
                ApplicationDate = a.ApplicationDate,
                ApplicationTypeID = a.ApplicationTypeID,
                ApplicationStatus = a.ApplicationStatus,
                LastStatusDate = a.LastStatusDate,
                PaidFees = a.PaidFees,
                CreatedByUserID = a.CreatedByUserID
            });

            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _applicationsService.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationResponseDto>> CreateApplication([FromBody] CreateApplicationDto dto)
        {
            var app = new AppEntity
            {
                ApplicantPersonID = dto.ApplicantPersonID,
                ApplicationTypeID = dto.ApplicationTypeID,
                PaidFees = dto.PaidFees,
                CreatedByUserID = dto.CreatedByUserID,
                ApplicationDate = System.DateTime.Now,
                ApplicationStatus = 1, // New
                LastStatusDate = System.DateTime.Now
            };

            var isCreated = await _applicationsService.CreateAsync(app);
            if (isCreated<0)
            {
                return BadRequest(new { Message = "Failed to create application." });
            }

            var response = new ApplicationResponseDto
            {
                ApplicationID = app.ApplicationID,
                ApplicantPersonID = app.ApplicantPersonID,
                ApplicationDate = app.ApplicationDate,
                ApplicationTypeID = app.ApplicationTypeID,
                ApplicationStatus = app.ApplicationStatus,
                LastStatusDate = app.LastStatusDate,
                PaidFees = app.PaidFees,
                CreatedByUserID = app.CreatedByUserID
            };

            return CreatedAtAction(nameof(GetApplicationById), new { id = app.ApplicationID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody] UpdateApplicationDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Application ID must be greater than 0." });
            }

            var existing = await _applicationsService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Application with ID {id} was not found." });
            }

            existing.ApplicantPersonID = dto.ApplicantPersonID;
            existing.ApplicationTypeID = dto.ApplicationTypeID;
            existing.ApplicationStatus = dto.ApplicationStatus;
            existing.PaidFees = dto.PaidFees;
            existing.CreatedByUserID = dto.CreatedByUserID;

            var isUpdated = await _applicationsService.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update application." });
            }

            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelApplication(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Application ID must be greater than 0." });
            }

            var isCancelled = await _applicationsService.CancelAsync(id);
            if (!isCancelled)
            {
                return BadRequest(new { Message = "Failed to cancel application." });
            }

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompleteApplication(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Application ID must be greater than 0." });
            }

            var isCompleted = await _applicationsService.SetCompleteAsync(id);
            if (!isCompleted)
            {
                return BadRequest(new { Message = "Failed to complete application." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Application ID must be greater than 0." });
            }

            var existing = await _applicationsService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Application with ID {id} was not found." });
            }

            var isDeleted = await _applicationsService.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Cannot delete this application because it has dependent records." });
            }

            return NoContent();
        }
    }
}
