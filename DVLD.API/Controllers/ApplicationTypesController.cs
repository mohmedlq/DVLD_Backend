using DVLD.Application.DTOs.ApplicationTypes;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationTypesController : ControllerBase
    {
        private readonly IApplicationTypesService _service;

        public ApplicationTypesController(IApplicationTypesService service)
        {
            _service = service;
        }

        #region Private Helpers

        private static bool IsValidApplicationType(enApplicationType id)
        {
            return Enum.IsDefined(typeof(enApplicationType), id);
        }

        #endregion

        #region GET Operations

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ApplicationTypeResponseDto>>> GetAllApplicationTypes()
        {
            var types = await _service.GetAllAsync();

            var response = types.Select(t => new ApplicationTypeResponseDto
            {
                ApplicationTypeID = t.ApplicationTypeID,
                ApplicationTypeTitle = t.ApplicationTypeTitle,
                ApplicationFees = t.ApplicationFees
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationTypeResponseDto>> GetApplicationTypeById(
            enApplicationType id)
        {
            if (!IsValidApplicationType(id))
            {
                return BadRequest(new
                {
                    Message = "Invalid Application Type ID."
                });
            }

            var type = await _service.GetByIdAsync(id);

            if (type is null)
            {
                return NotFound(new
                {
                    Message = $"Application Type with ID {id} was not found."
                });
            }

            var response = new ApplicationTypeResponseDto
            {
                ApplicationTypeID = type.ApplicationTypeID,
                ApplicationTypeTitle = type.ApplicationTypeTitle,
                ApplicationFees = type.ApplicationFees
            };

            return Ok(response);
        }

        [HttpGet("{id}/fees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<decimal>> GetApplicationFees(
            enApplicationType id)
        {
            if (!IsValidApplicationType(id))
            {
                return BadRequest(new
                {
                    Message = "Invalid Application Type ID."
                });
            }

            var fees = await _service.GetApplicationFeesAsync(id);

            if (!fees.HasValue)
            {
                return NotFound(new
                {
                    Message = $"Application Type with ID {id} was not found."
                });
            }

            return Ok(fees.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _service.CountAsync();

            return Ok(count);
        }

        #endregion

        #region POST Operation

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplicationTypeResponseDto>> CreateApplicationType(
            [FromBody] CreateApplicationTypeDto dto)
        {
            var applicationType = new ApplicationType
            {
                ApplicationTypeTitle = dto.ApplicationTypeTitle,
                ApplicationFees = dto.ApplicationFees
            };

            var isCreated = await _service.CreateAsync(applicationType);

            if (!isCreated)
            {
                return BadRequest(new
                {
                    Message = "Failed to create application type."
                });
            }

            var response = new ApplicationTypeResponseDto
            {
                ApplicationTypeID = applicationType.ApplicationTypeID,
                ApplicationTypeTitle = applicationType.ApplicationTypeTitle,
                ApplicationFees = applicationType.ApplicationFees
            };

            return CreatedAtAction(
                nameof(GetApplicationTypeById),
                new
                {
                    id = (enApplicationType)applicationType.ApplicationTypeID
                },
                response
            );
        }

        #endregion

        #region PUT Operation

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplicationType(
            enApplicationType id,
            [FromBody] UpdateApplicationTypeDto dto)
        {
            if (!IsValidApplicationType(id))
            {
                return BadRequest(new
                {
                    Message = "Invalid Application Type ID."
                });
            }

            var existing = await _service.GetByIdAsync(id);

            if (existing is null)
            {
                return NotFound(new
                {
                    Message = $"Application Type with ID {id} was not found."
                });
            }

            existing.ApplicationTypeTitle = dto.ApplicationTypeTitle;
            existing.ApplicationFees = dto.ApplicationFees;

            var isUpdated = await _service.UpdateAsync(existing);

            if (!isUpdated)
            {
                return BadRequest(new
                {
                    Message = "Failed to update application type."
                });
            }

            return NoContent();
        }

        #endregion

        #region DELETE Operation

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteApplicationType(
            enApplicationType id)
        {
            if (!IsValidApplicationType(id))
            {
                return BadRequest(new
                {
                    Message = "Invalid Application Type ID."
                });
            }

            var existing = await _service.GetByIdAsync(id);

            if (existing is null)
            {
                return NotFound(new
                {
                    Message = $"Application Type with ID {id} was not found."
                });
            }

            var isDeleted = await _service.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest(new
                {
                    Message = "Cannot delete this application type because it is referenced by existing applications."
                });
            }

            return NoContent();
        }

        #endregion
    }
}