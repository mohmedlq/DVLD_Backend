using TaskEntity = DVLD.Domain.Entities.Tasks;
using DVLD.Application.DTOs.Tasks;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _service;

        public TasksController(ITasksService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAll()
        {
            var tasks = await _service.GetAllAsync();
            var response = tasks.Select(t => new TaskResponseDto
            {
                TaskID = t.TaskID,
                UserID = t.UserID,
                TaskTitle = t.TaskTitle,
                Description = t.Description,
                AssignedDate = t.AssignedDate,
                DueDate = t.DueDate,
                Status = t.Status
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var t = await _service.GetByIdAsync(id);
            if (t is null)
            {
                return NotFound(new { Message = $"Task with ID {id} was not found." });
            }

            var response = new TaskResponseDto
            {
                TaskID = t.TaskID,
                UserID = t.UserID,
                TaskTitle = t.TaskTitle,
                Description = t.Description,
                AssignedDate = t.AssignedDate,
                DueDate = t.DueDate,
                Status = t.Status
            };

            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetByUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { Message = "User ID must be greater than 0." });
            }

            var tasks = await _service.GetByUserIdAsync(userId);
            var response = tasks.Select(t => new TaskResponseDto
            {
                TaskID = t.TaskID,
                UserID = t.UserID,
                TaskTitle = t.TaskTitle,
                Description = t.Description,
                AssignedDate = t.AssignedDate,
                DueDate = t.DueDate,
                Status = t.Status
            });

            return Ok(response);
        }

        [HttpGet("pending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetPending()
        {
            var tasks = await _service.GetPendingTasksAsync();
            var response = tasks.Select(t => new TaskResponseDto
            {
                TaskID = t.TaskID,
                UserID = t.UserID,
                TaskTitle = t.TaskTitle,
                Description = t.Description,
                AssignedDate = t.AssignedDate,
                DueDate = t.DueDate,
                Status = t.Status
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
        public async Task<ActionResult<TaskResponseDto>> Create([FromBody] CreateTaskDto dto)
        {
            var task = new TaskEntity
            {
                UserID = dto.UserID,
                TaskTitle = dto.TaskTitle,
                Description = dto.Description,
                AssignedDate = System.DateOnly.FromDateTime(System.DateTime.Now),
                DueDate = dto.DueDate,
                Status = false
            };

            var isCreated = await _service.CreateAsync(task);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create task." });
            }

            var response = new TaskResponseDto
            {
                TaskID = task.TaskID,
                UserID = task.UserID,
                TaskTitle = task.TaskTitle,
                Description = task.Description,
                AssignedDate = task.AssignedDate,
                DueDate = task.DueDate,
                Status = task.Status
            };

            return CreatedAtAction(nameof(GetById), new { id = task.TaskID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Task with ID {id} was not found." });
            }

            existing.UserID = dto.UserID;
            existing.TaskTitle = dto.TaskTitle;
            existing.Description = dto.Description;
            existing.DueDate = dto.DueDate;
            existing.Status = dto.Status;

            var isUpdated = await _service.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update task." });
            }

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Complete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Task with ID {id} was not found." });
            }

            var isCompleted = await _service.CompleteTaskAsync(id);
            if (!isCompleted)
            {
                return BadRequest(new { Message = "Failed to complete task." });
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
                return NotFound(new { Message = $"Task with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete task." });
            }

            return NoContent();
        }
    }
}

