using DVLD.Application.DTOs.TestTypes;
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
    public class TestTypesController : ControllerBase
    {
        private readonly ITestTypesService _testTypesService;

        public TestTypesController(ITestTypesService testTypesService)
        {
            _testTypesService = testTypesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TestTypeResponseDto>>> GetAllTestTypes()
        {
            var testTypes = await _testTypesService.GetAllAsync();
            var response = testTypes.Select(t => new TestTypeResponseDto
            {
                TestTypeID = t.TestTypeID,
                TestTypeTitle = t.TestTypeTitle,
                TestTypeDescription = t.TestTypeDescription,
                TestTypeFees = t.TestTypeFees
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestTypeResponseDto>> GetTestTypeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Test Type ID must be greater than 0." });
            }

            var testType = await _testTypesService.GetByIdAsync(id);
            if (testType is null)
            {
                return NotFound(new { Message = $"Test Type with ID {id} was not found." });
            }

            var response = new TestTypeResponseDto
            {
                TestTypeID = testType.TestTypeID,
                TestTypeTitle = testType.TestTypeTitle,
                TestTypeDescription = testType.TestTypeDescription,
                TestTypeFees = testType.TestTypeFees
            };

            return Ok(response);
        }

        [HttpGet("{id}/fees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<decimal>> GetTestTypeFees(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Test Type ID must be greater than 0." });
            }

            var fees = await _testTypesService.GetTestTypeFeesAsync(id);
            if (!fees.HasValue)
            {
                return NotFound(new { Message = $"Test Type with ID {id} was not found." });
            }

            return Ok(fees.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _testTypesService.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TestTypeResponseDto>> CreateTestType([FromBody] CreateTestTypeDto dto)
        {
            var testType = new TestType
            {
                TestTypeTitle = dto.TestTypeTitle,
                TestTypeDescription = dto.TestTypeDescription,
                TestTypeFees = dto.TestTypeFees
            };

            var isCreated = await _testTypesService.CreateAsync(testType);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create test type." });
            }

            var response = new TestTypeResponseDto
            {
                TestTypeID = testType.TestTypeID,
                TestTypeTitle = testType.TestTypeTitle,
                TestTypeDescription = testType.TestTypeDescription,
                TestTypeFees = testType.TestTypeFees
            };

            return CreatedAtAction(nameof(GetTestTypeById), new { id = testType.TestTypeID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTestType(int id, [FromBody] UpdateTestTypeDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Test Type ID must be greater than 0." });
            }

            var existing = await _testTypesService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Test Type with ID {id} was not found." });
            }

            existing.TestTypeTitle = dto.TestTypeTitle;
            existing.TestTypeDescription = dto.TestTypeDescription;
            existing.TestTypeFees = dto.TestTypeFees;

            var isUpdated = await _testTypesService.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update test type." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTestType(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Test Type ID must be greater than 0." });
            }

            var existing = await _testTypesService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Test Type with ID {id} was not found." });
            }

            var isDeleted = await _testTypesService.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Cannot delete this test type because it is referenced by existing test appointments." });
            }

            return NoContent();
        }
    }
}

