using DVLD.Application.DTOs.Logs;
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
    public class LogsController : ControllerBase
    {
        private readonly ILogsService _service;

        public LogsController(ILogsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LogResponseDto>>> GetAll()
        {
            var logs = await _service.GetAllAsync();
            var response = logs.Select(l => new LogResponseDto
            {
                LogId = l.LogId,
                Level = l.Level,
                Message = l.Message,
                ExceptionType = l.ExceptionType,
                StackTrace = l.StackTrace,
                RequestPath = l.RequestPath,
                HttpMethod = l.HttpMethod,
                UserId = l.UserId,
                CreatedAt = l.CreatedAt
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LogResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var l = await _service.GetByIdAsync(id);
            if (l is null)
            {
                return NotFound(new { Message = $"Log with ID {id} was not found." });
            }

            var response = new LogResponseDto
            {
                LogId = l.LogId,
                Level = l.Level,
                Message = l.Message,
                ExceptionType = l.ExceptionType,
                StackTrace = l.StackTrace,
                RequestPath = l.RequestPath,
                HttpMethod = l.HttpMethod,
                UserId = l.UserId,
                CreatedAt = l.CreatedAt
            };

            return Ok(response);
        }

        [HttpGet("level/{level}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LogResponseDto>>> GetByLevel(string level)
        {
            if (string.IsNullOrWhiteSpace(level))
            {
                return BadRequest(new { Message = "Level cannot be empty." });
            }

            var logs = await _service.GetByLevelAsync(level);
            var response = logs.Select(l => new LogResponseDto
            {
                LogId = l.LogId,
                Level = l.Level,
                Message = l.Message,
                ExceptionType = l.ExceptionType,
                StackTrace = l.StackTrace,
                RequestPath = l.RequestPath,
                HttpMethod = l.HttpMethod,
                UserId = l.UserId,
                CreatedAt = l.CreatedAt
            });

            return Ok(response);
        }

        [HttpGet("recent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LogResponseDto>>> GetRecent([FromQuery] int count = 50)
        {
            var logs = await _service.GetRecentLogsAsync(count);
            var response = logs.Select(l => new LogResponseDto
            {
                LogId = l.LogId,
                Level = l.Level,
                Message = l.Message,
                ExceptionType = l.ExceptionType,
                StackTrace = l.StackTrace,
                RequestPath = l.RequestPath,
                HttpMethod = l.HttpMethod,
                UserId = l.UserId,
                CreatedAt = l.CreatedAt
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
        public async Task<ActionResult<LogResponseDto>> Create([FromBody] CreateLogDto dto)
        {
            var log = new Log
            {
                Level = dto.Level,
                Message = dto.Message,
                ExceptionType = dto.ExceptionType,
                StackTrace = dto.StackTrace,
                RequestPath = dto.RequestPath,
                HttpMethod = dto.HttpMethod,
                UserId = dto.UserId,
                CreatedAt = System.DateTime.Now
            };

            var isCreated = await _service.CreateAsync(log);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create log entry." });
            }

            var response = new LogResponseDto
            {
                LogId = log.LogId,
                Level = log.Level,
                Message = log.Message,
                ExceptionType = log.ExceptionType,
                StackTrace = log.StackTrace,
                RequestPath = log.RequestPath,
                HttpMethod = log.HttpMethod,
                UserId = log.UserId,
                CreatedAt = log.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = log.LogId }, response);
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
                return NotFound(new { Message = $"Log with ID {id} was not found." });
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

