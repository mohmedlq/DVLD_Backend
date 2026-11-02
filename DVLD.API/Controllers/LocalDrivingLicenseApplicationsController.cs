using DVLD.Application.DTOs.LocalDrivingLicenseApplications;
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
    public class LocalDrivingLicenseApplicationsController : ControllerBase
    {
        private readonly ILocalDrivingLicenseApplicationsService _service;

        public LocalDrivingLicenseApplicationsController(ILocalDrivingLicenseApplicationsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LocalDrivingLicenseApplicationResponseDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            var response = items.Select(x => new LocalDrivingLicenseApplicationResponseDto
            {
                LocalDrivingLicenseApplicationID = x.LocalDrivingLicenseApplicationID,
                ApplicationID = x.ApplicationID,
                LicenseClassID = x.LicenseClassID
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LocalDrivingLicenseApplicationResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var item = await _service.GetByIdAsync(id);
            if (item is null)
            {
                return NotFound(new { Message = $"Local Driving License Application with ID {id} was not found." });
            }

            var response = new LocalDrivingLicenseApplicationResponseDto
            {
                LocalDrivingLicenseApplicationID = item.LocalDrivingLicenseApplicationID,
                ApplicationID = item.ApplicationID,
                LicenseClassID = item.LicenseClassID
            };

            return Ok(response);
        }

        [HttpGet("application/{applicationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LocalDrivingLicenseApplicationResponseDto>> GetByApplicationId(int applicationId)
        {
            if (applicationId <= 0)
            {
                return BadRequest(new { Message = "Application ID must be greater than 0." });
            }

            var item = await _service.GetByApplicationIdAsync(applicationId);
            if (item is null)
            {
                return NotFound(new { Message = $"Local Driving License Application with Application ID {applicationId} was not found." });
            }

            var response = new LocalDrivingLicenseApplicationResponseDto
            {
                LocalDrivingLicenseApplicationID = item.LocalDrivingLicenseApplicationID,
                ApplicationID = item.ApplicationID,
                LicenseClassID = item.LicenseClassID
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
        public async Task<ActionResult<LocalDrivingLicenseApplicationResponseDto>> Create([FromBody] CreateLocalDrivingLicenseApplicationRequest dto)
        {
           
            var isCreated = await _service.CreateAsync(dto);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create local driving license application." });
            }

         

            return Ok("Created Seccssfully");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLocalDrivingLicenseApplicationDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Local Driving License Application with ID {id} was not found." });
            }

            existing.ApplicationID = dto.ApplicationID;
            existing.LicenseClassID = dto.LicenseClassID;

            var isUpdated = await _service.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update local driving license application." });
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
                return NotFound(new { Message = $"Local Driving License Application with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Cannot delete this record because it has related appointments or tests." });
            }

            return NoContent();
        }
    }
}

