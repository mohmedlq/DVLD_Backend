using DVLD.Application.DTOs.Users;
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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var users = await _usersService.GetAllAsync();
            var response = users.Select(u => new UserResponseDto
            {
                UserID = u.UserID,
                PersonID = u.PersonID,
                UserName = u.UserName,
                IsActive = u.IsActive,
                Permissions = u.Permissions
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "User ID must be greater than 0." });
            }

            var user = await _usersService.GetByIdAsync(id);
            if (user is null)
            {
                return NotFound(new { Message = $"User with ID {id} was not found." });
            }

            var response = new UserResponseDto
            {
                UserID = user.UserID,
                PersonID = user.PersonID,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Permissions = user.Permissions
            };

            return Ok(response);
        }

        [HttpGet("person/{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetUserByPersonId(int personId)
        {
            if (personId <= 0)
            {
                return BadRequest(new { Message = "Person ID must be greater than 0." });
            }

            var user = await _usersService.GetByPersonIdAsync(personId);
            if (user is null)
            {
                return NotFound(new { Message = $"User with Person ID {personId} was not found." });
            }

            var response = new UserResponseDto
            {
                UserID = user.UserID,
                PersonID = user.PersonID,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Permissions = user.Permissions
            };

            return Ok(response);
        }

        [HttpGet("status/{isActive}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsersByStatus(bool isActive)
        {
            var users = await _usersService.GetByActiveStateAsync(isActive);
            var response = users.Select(u => new UserResponseDto
            {
                UserID = u.UserID,
                PersonID = u.PersonID,
                UserName = u.UserName,
                IsActive = u.IsActive,
                Permissions = u.Permissions
            });

            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetCount()
        {
            var count = await _usersService.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] CreateUserDto dto)
        {
            var user = new User
            {
                PersonID = dto.PersonID,
                UserName = dto.UserName,
                Password = dto.Password,
                IsActive = dto.IsActive,
                Permissions = dto.Permissions
            };

            var isCreated = await _usersService.CreateAsync(user);
            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create user." });
            }

            var response = new UserResponseDto
            {
                UserID = user.UserID,
                PersonID = user.PersonID,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Permissions = user.Permissions
            };

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "User ID must be greater than 0." });
            }

            var existing = await _usersService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"User with ID {id} was not found." });
            }

            existing.PersonID = dto.PersonID;
            existing.UserName = dto.UserName;
            existing.IsActive = dto.IsActive;

            var isUpdated = await _usersService.UpdateAsync(existing);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update user." });
            }

            return NoContent();
        }

        [HttpPut("{id}/change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] UserChangePasswordDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "User ID must be greater than 0." });
            }

            var existing = await _usersService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"User with ID {id} was not found." });
            }

            var isChanged = await _usersService.ChangePasswordAsync(id, dto.NewPassword);
            if (!isChanged)
            {
                return BadRequest(new { Message = "Failed to change password." });
            }

            return NoContent();
        }

        [HttpPut("{id}/permissions")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePermissions(int id, [FromBody] UpdatePermissionsDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "User ID must be greater than 0." });
            }

            var existing = await _usersService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"User with ID {id} was not found." });
            }

            var isUpdated = await _usersService.UpdatePermissionsAsync(id, dto.Permissions);
            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update permissions." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "User ID must be greater than 0." });
            }

            var existing = await _usersService.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound(new { Message = $"User with ID {id} was not found." });
            }

            var isDeleted = await _usersService.DeleteAsync(id);
            if (!isDeleted)
            {
                return BadRequest(new { Message = "Failed to delete user." });
            }

            return NoContent();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserResponseDto>> Login([FromBody] LoginDto dto)
        {
            var user = await _usersService.GetByUsernameAndPasswordAsync(dto.UserName, dto.Password);
            if (user is null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            if (user.IsActive==false)
            {
                return Unauthorized(new { Message = "User account is inactive. Please contact your administrator." });
            }

            var response = new UserResponseDto
            {
                UserID = user.UserID,
                PersonID = user.PersonID,
                UserName = user.UserName,
                IsActive = user.IsActive,
                Permissions = user.Permissions
            };

            return Ok(response);
        }
    }
}
