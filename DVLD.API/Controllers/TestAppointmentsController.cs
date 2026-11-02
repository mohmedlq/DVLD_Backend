using DVLD.Application.DTOs.TestAppointments;
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
    public class TestAppointmentsController : ControllerBase
    {
        private readonly ITestAppointmentsService _service;

        public TestAppointmentsController(ITestAppointmentsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TestAppointmentResponseDto>>> GetAll()
        {
            var appointments = await _service.GetAllAsync();
            var response = appointments.Select(a => new TestAppointmentResponseDto
            {
                TestAppointmentID = a.TestAppointmentID,
                TestTypeID = a.TestTypeID,
                LocalDrivingLicenseApplicationID = a.LocalDrivingLicenseApplicationID,
                AppointmentDate = a.AppointmentDate,
                PaidFees = a.PaidFees,
                CreatedByUserID = a.CreatedByUserID,
                IsLocked = a.IsLocked,
                RetakeTestApplicationID = a.RetakeTestApplicationID
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestAppointmentResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var a = await _service.GetByIdAsync(id);
            if (a is null)
            {
                return NotFound(new { Message = $"Test Appointment with ID {id} was not found." });
            }

            var response = new TestAppointmentResponseDto
            {
                TestAppointmentID = a.TestAppointmentID,
                TestTypeID = a.TestTypeID,
                LocalDrivingLicenseApplicationID = a.LocalDrivingLicenseApplicationID,
                AppointmentDate = a.AppointmentDate,
                PaidFees = a.PaidFees,
                CreatedByUserID = a.CreatedByUserID,
                IsLocked = a.IsLocked,
                RetakeTestApplicationID = a.RetakeTestApplicationID
            };

            return Ok(response);
        }

        [HttpGet("local-application/{localAppId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TestAppointmentResponseDto>>> GetByLocalApplicationId(int localAppId)
        {
            if (localAppId <= 0)
            {
                return BadRequest(new { Message = "Local Driving License Application ID must be greater than 0." });
            }

            var appointments = await _service.GetByLocalDrivingLicenseApplicationIdAsync(localAppId);
            var response = appointments.Select(a => new TestAppointmentResponseDto
            {
                TestAppointmentID = a.TestAppointmentID,
                TestTypeID = a.TestTypeID,
                LocalDrivingLicenseApplicationID = a.LocalDrivingLicenseApplicationID,
                AppointmentDate = a.AppointmentDate,
                PaidFees = a.PaidFees,
                CreatedByUserID = a.CreatedByUserID,
                IsLocked = a.IsLocked,
                RetakeTestApplicationID = a.RetakeTestApplicationID
            });

            return Ok(response);
        }

        [HttpGet("test-type/{testTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TestAppointmentResponseDto>>> GetByTestTypeId(int testTypeId)
        {
            if (testTypeId <= 0)
            {
                return BadRequest(new { Message = "Test Type ID must be greater than 0." });
            }

            var appointments = await _service.GetByTestTypeIdAsync(testTypeId);
            var response = appointments.Select(a => new TestAppointmentResponseDto
            {
                TestAppointmentID = a.TestAppointmentID,
                TestTypeID = a.TestTypeID,
                LocalDrivingLicenseApplicationID = a.LocalDrivingLicenseApplicationID,
                AppointmentDate = a.AppointmentDate,
                PaidFees = a.PaidFees,
                CreatedByUserID = a.CreatedByUserID,
                IsLocked = a.IsLocked,
                RetakeTestApplicationID = a.RetakeTestApplicationID
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
        public async Task<ActionResult<TestAppointmentResponseDto>> Create([FromBody] CreateTestAppointmentDto dto)
        {
            var appointment = new TestAppointment
            {
                TestTypeID = dto.TestTypeID,
                LocalDrivingLicenseApplicationID = dto.LocalDrivingLicenseApplicationID,
                AppointmentDate = dto.AppointmentDate == default ? System.DateTime.Now : dto.AppointmentDate,
                PaidFees = dto.PaidFees,
                CreatedByUserID = dto.CreatedByUserID,
                IsLocked = false,
                RetakeTestApplicationID = dto.RetakeTestApplicationID
            };

            var isCreated = await _service.CreateAsync(appointment);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create test appointment." });
            }

            var response = new TestAppointmentResponseDto
            {
                TestAppointmentID = appointment.TestAppointmentID,
                TestTypeID = appointment.TestTypeID,
                LocalDrivingLicenseApplicationID = appointment.LocalDrivingLicenseApplicationID,
                AppointmentDate = appointment.AppointmentDate,
                PaidFees = appointment.PaidFees,
                CreatedByUserID = appointment.CreatedByUserID,
                IsLocked = appointment.IsLocked,
                RetakeTestApplicationID = appointment.RetakeTestApplicationID
            };

            return CreatedAtAction(nameof(GetById), new { id = appointment.TestAppointmentID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTestAppointmentDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Test Appointment with ID {id} was not found." });
            }

            existing.AppointmentDate = dto.AppointmentDate;
            existing.PaidFees = dto.PaidFees;
            existing.CreatedByUserID = dto.CreatedByUserID;
            existing.RetakeTestApplicationID = dto.RetakeTestApplicationID;

            var isUpdated = await _service.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update test appointment." });
            }

            return NoContent();
        }

        [HttpPut("{id}/lock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Lock(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Test Appointment with ID {id} was not found." });
            }

            var isLocked = await _service.LockAppointmentAsync(id);
            if (!isLocked)
            {
                return BadRequest(new { Message = "Failed to lock test appointment." });
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
                return NotFound(new { Message = $"Test Appointment with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Cannot delete this test appointment because it has related test records or is locked." });
            }

            return NoContent();
        }
    }
}

