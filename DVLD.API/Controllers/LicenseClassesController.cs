using DVLD.Application.DTOs.LicenseClasses;
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
    public class LicenseClassesController : ControllerBase
    {
        private readonly ILicenseClassesService _service;

        public LicenseClassesController(ILicenseClassesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LicenseClassResponseDto>>> GetAllLicenseClasses()
        {
            var classes = await _service.GetAllAsync();
            var response = classes.Select(c => new LicenseClassResponseDto
            {
                LicenseClassID = c.LicenseClassID,
                ClassName = c.ClassName,
                ClassDescription = c.ClassDescription,
                MinimumAllowedAge = c.MinimumAllowedAge,
                DefaultValidityLength = c.DefaultValidityLength,
                ClassFees = c.ClassFees
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LicenseClassResponseDto>> GetLicenseClassById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var licenseClass = await _service.GetByIdAsync(id);
            if (licenseClass is null)
            {
                return NotFound(new { Message = $"License Class with ID {id} was not found." });
            }

            var response = new LicenseClassResponseDto
            {
                LicenseClassID = licenseClass.LicenseClassID,
                ClassName = licenseClass.ClassName,
                ClassDescription = licenseClass.ClassDescription,
                MinimumAllowedAge = licenseClass.MinimumAllowedAge,
                DefaultValidityLength = licenseClass.DefaultValidityLength,
                ClassFees = licenseClass.ClassFees
            };

            return Ok(response);
        }

        [HttpGet("name/{className}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LicenseClassResponseDto>> GetLicenseClassByName(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return BadRequest(new { Message = "Class name cannot be empty." });
            }

            var licenseClass = await _service.GetByClassNameAsync(className);
            if (licenseClass is null)
            {
                return NotFound(new { Message = $"License Class with name '{className}' was not found." });
            }

            var response = new LicenseClassResponseDto
            {
                LicenseClassID = licenseClass.LicenseClassID,
                ClassName = licenseClass.ClassName,
                ClassDescription = licenseClass.ClassDescription,
                MinimumAllowedAge = licenseClass.MinimumAllowedAge,
                DefaultValidityLength = licenseClass.DefaultValidityLength,
                ClassFees = licenseClass.ClassFees
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
        public async Task<ActionResult<LicenseClassResponseDto>> CreateLicenseClass([FromBody] CreateLicenseClassDto dto)
        {
            var licenseClass = new LicenseClass
            {
                ClassName = dto.ClassName,
                ClassDescription = dto.ClassDescription,
                MinimumAllowedAge = dto.MinimumAllowedAge,
                DefaultValidityLength = dto.DefaultValidityLength,
                ClassFees = dto.ClassFees
            };

            var isCreated = await _service.CreateAsync(licenseClass);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create license class." });
            }

            var response = new LicenseClassResponseDto
            {
                LicenseClassID = licenseClass.LicenseClassID,
                ClassName = licenseClass.ClassName,
                ClassDescription = licenseClass.ClassDescription,
                MinimumAllowedAge = licenseClass.MinimumAllowedAge,
                DefaultValidityLength = licenseClass.DefaultValidityLength,
                ClassFees = licenseClass.ClassFees
            };

            return CreatedAtAction(nameof(GetLicenseClassById), new { id = licenseClass.LicenseClassID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLicenseClass(int id, [FromBody] UpdateLicenseClassDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"License Class with ID {id} was not found." });
            }

            existing.ClassName = dto.ClassName;
            existing.ClassDescription = dto.ClassDescription;
            existing.MinimumAllowedAge = dto.MinimumAllowedAge;
            existing.DefaultValidityLength = dto.DefaultValidityLength;
            existing.ClassFees = dto.ClassFees;

            var isUpdated = await _service.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update license class." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLicenseClass(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"License Class with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Cannot delete this license class because it is referenced by existing licenses or applications." });
            }

            return NoContent();
        }
    }
}

