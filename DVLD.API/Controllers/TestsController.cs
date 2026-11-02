using DVLD.Application.DTOs.Tests;
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
    public class TestsController : ControllerBase
    {
        private readonly ITestsService _testsService;

        public TestsController(ITestsService testsService)
        {
            _testsService = testsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TestResponseDto>>> GetAllTests()
        {
            var tests = await _testsService.GetAllAsync();
            var response = tests.Select(t => new TestResponseDto
            {
                TestID = t.TestID,
                TestAppointmentID = t.TestAppointmentID,
                TestResult = t.TestResult,
                Notes = t.Notes,
                CreatedByUserID = t.CreatedByUserID
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestResponseDto>> GetTestById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Test ID must be greater than 0." });
            }

            var test = await _testsService.GetByIdAsync(id);
            if (test is null)
            {
                return NotFound(new { Message = $"Test with ID {id} was not found." });
            }

            var response = new TestResponseDto
            {
                TestID = test.TestID,
                TestAppointmentID = test.TestAppointmentID,
                TestResult = test.TestResult,
                Notes = test.Notes,
                CreatedByUserID = test.CreatedByUserID
            };

            return Ok(response);
        }

        [HttpGet("appointment/{appointmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestResponseDto>> GetTestByAppointmentId(int appointmentId)
        {
            if (appointmentId <= 0)
            {
                return BadRequest(new { Message = "Test Appointment ID must be greater than 0." });
            }

            var test = await _testsService.GetByAppointmentIdAsync(appointmentId);
            if (test is null)
            {
                return NotFound(new { Message = $"Test for Appointment ID {appointmentId} was not found." });
            }

            var response = new TestResponseDto
            {
                TestID = test.TestID,
                TestAppointmentID = test.TestAppointmentID,
                TestResult = test.TestResult,
                Notes = test.Notes,
                CreatedByUserID = test.CreatedByUserID
            };

            return Ok(response);
        }

        [HttpGet("passed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TestResponseDto>>> GetPassedTests()
        {
            var tests = await _testsService.GetPassedTestsAsync();
            var response = tests.Select(t => new TestResponseDto
            {
                TestID = t.TestID,
                TestAppointmentID = t.TestAppointmentID,
                TestResult = t.TestResult,
                Notes = t.Notes,
                CreatedByUserID = t.CreatedByUserID
            });

            return Ok(response);
        }

        [HttpGet("local-application/{localAppId}/passed-count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetPassedTestCount(int localAppId)
        {
            if (localAppId <= 0)
            {
                return BadRequest(new { Message = "Local Driving License Application ID must be greater than 0." });
            }

            var count = await _testsService.GetPassedTestCountAsync(localAppId);
            return Ok(count);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _testsService.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TestResponseDto>> CreateTest([FromBody] CreateTestDto dto)
        {
            var test = new Test
            {
                TestAppointmentID = dto.TestAppointmentID,
                TestResult = dto.TestResult,
                Notes = dto.Notes,
                CreatedByUserID = dto.CreatedByUserID
            };

            var isCreated = await _testsService.CreateAsync(test);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to record test result." });
            }

            var response = new TestResponseDto
            {
                TestID = test.TestID,
                TestAppointmentID = test.TestAppointmentID,
                TestResult = test.TestResult,
                Notes = test.Notes,
                CreatedByUserID = test.CreatedByUserID
            };

            return CreatedAtAction(nameof(GetTestById), new { id = test.TestID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTest(int id, [FromBody] UpdateTestDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Test ID must be greater than 0." });
            }

            var existing = await _testsService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Test with ID {id} was not found." });
            }

            existing.TestAppointmentID = dto.TestAppointmentID;
            existing.TestResult = dto.TestResult;
            existing.Notes = dto.Notes;
            existing.CreatedByUserID = dto.CreatedByUserID;

            var isUpdated = await _testsService.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update test record." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTest(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "Test ID must be greater than 0." });
            }

            var existing = await _testsService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Test with ID {id} was not found." });
            }

            var isDeleted = await _testsService.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete test record." });
            }

            return NoContent();
        }
    }
}

