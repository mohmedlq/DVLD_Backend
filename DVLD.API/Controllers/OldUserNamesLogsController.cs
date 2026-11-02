using DVLD.Application.DTOs.OldUserNamesLogs;
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
    public class OldUserNamesLogsController : ControllerBase
    {
        private readonly IOldUserNamesLogsService _service;

        public OldUserNamesLogsController(IOldUserNamesLogsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OldUserNamesLogResponseDto>>> GetAll()
        {
            var logs = await _service.GetAllAsync();
            var response = logs.Select(x => new OldUserNamesLogResponseDto
            {
                LogID = x.LogID,
                OldUserName = x.OldUserName,
                NewUserName = x.NewUserName
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OldUserNamesLogResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var log = await _service.GetByIdAsync(id);
            if (log is null)
            {
                return NotFound(new { Message = $"Old username log with ID {id} was not found." });
            }

            var response = new OldUserNamesLogResponseDto
            {
                LogID = log.LogID,
                OldUserName = log.OldUserName,
                NewUserName = log.NewUserName
            };

            return Ok(response);
        }

        [HttpGet("username/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OldUserNamesLogResponseDto>>> GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new { Message = "Username cannot be empty." });
            }

            var logs = await _service.GetByUsernameAsync(username);
            var response = logs.Select(x => new OldUserNamesLogResponseDto
            {
                LogID = x.LogID,
                OldUserName = x.OldUserName,
                NewUserName = x.NewUserName
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
        public async Task<ActionResult<OldUserNamesLogResponseDto>> Create([FromBody] CreateOldUserNamesLogDto dto)
        {
            var log = new OldUserNamesLog
            {
                OldUserName = dto.OldUserName,
                NewUserName = dto.NewUserName
            };

            var isCreated = await _service.CreateAsync(log);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create log entry." });
            }

            var response = new OldUserNamesLogResponseDto
            {
                LogID = log.LogID,
                OldUserName = log.OldUserName,
                NewUserName = log.NewUserName
            };

            return CreatedAtAction(nameof(GetById), new { id = log.LogID }, response);
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
                return NotFound(new { Message = $"Old username log with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete log entry." });
            }

            return NoContent();
        }
    }
}

