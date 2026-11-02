using DVLD.Application.DTOs.Massages;
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
    public class MassagesController : ControllerBase
    {
        private readonly IMassagesService _service;

        public MassagesController(IMassagesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MassageResponseDto>>> GetAll()
        {
            var msgs = await _service.GetAllAsync();
            var response = msgs.Select(m => new MassageResponseDto
            {
                MessageID = m.MessageID,
                PersonID = m.PersonID,
                MessageTitle = m.MessageTitle,
                MessageContent = m.MessageContent,
                IsRead = m.IsRead
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MassageResponseDto>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var m = await _service.GetByIdAsync(id);
            if (m is null)
            {
                return NotFound(new { Message = $"Message with ID {id} was not found." });
            }

            var response = new MassageResponseDto
            {
                MessageID = m.MessageID,
                PersonID = m.PersonID,
                MessageTitle = m.MessageTitle,
                MessageContent = m.MessageContent,
                IsRead = m.IsRead
            };

            return Ok(response);
        }

        [HttpGet("person/{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MassageResponseDto>>> GetByPersonId(int personId)
        {
            if (personId <= 0)
            {
                return BadRequest(new { Message = "Person ID must be greater than 0." });
            }

            var msgs = await _service.GetByPersonIdAsync(personId);
            var response = msgs.Select(m => new MassageResponseDto
            {
                MessageID = m.MessageID,
                PersonID = m.PersonID,
                MessageTitle = m.MessageTitle,
                MessageContent = m.MessageContent,
                IsRead = m.IsRead
            });

            return Ok(response);
        }

        [HttpGet("person/{personId}/unread")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MassageResponseDto>>> GetUnreadByPersonId(int personId)
        {
            if (personId <= 0)
            {
                return BadRequest(new { Message = "Person ID must be greater than 0." });
            }

            var msgs = await _service.GetUnreadByPersonIdAsync(personId);
            var response = msgs.Select(m => new MassageResponseDto
            {
                MessageID = m.MessageID,
                PersonID = m.PersonID,
                MessageTitle = m.MessageTitle,
                MessageContent = m.MessageContent,
                IsRead = m.IsRead
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
        public async Task<ActionResult<MassageResponseDto>> Create([FromBody] CreateMassageDto dto)
        {
            var msg = new Massage
            {
                PersonID = dto.PersonID,
                MessageTitle = dto.MessageTitle,
                MessageContent = dto.MessageContent,
                IsRead = false
            };

            var isCreated = await _service.CreateAsync(msg);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to send message." });
            }

            var response = new MassageResponseDto
            {
                MessageID = msg.MessageID,
                PersonID = msg.PersonID,
                MessageTitle = msg.MessageTitle,
                MessageContent = msg.MessageContent,
                IsRead = msg.IsRead
            };

            return CreatedAtAction(nameof(GetById), new { id = msg.MessageID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMassageDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Message with ID {id} was not found." });
            }

            existing.MessageTitle = dto.MessageTitle;
            existing.MessageContent = dto.MessageContent;
            existing.IsRead = dto.IsRead;

            var isUpdated = await _service.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update message." });
            }

            return NoContent();
        }

        [HttpPut("{id}/read")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "ID must be greater than 0." });
            }

            var existing = await _service.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"Message with ID {id} was not found." });
            }

            var isRead = await _service.MarkAsReadAsync(id);
            if (!isRead)
            {
                return BadRequest(new { Message = "Failed to mark message as read." });
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
                return NotFound(new { Message = $"Message with ID {id} was not found." });
            }

            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete message." });
            }

            return NoContent();
        }
    }
}

