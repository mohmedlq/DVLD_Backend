using DVLD.Business.DTOs.People;
using DVLD.Business.Interfaces;
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
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PersonResponseDto>>> GetAllPeople()
        {
            var people = await _peopleService.GetAllAsync();

            var response = people.Select(p => new PersonResponseDto
            {
                PersonID = p.PersonID,
                NationalNo = p.NationalNo,
                FirstName = p.FirstName,
                SecondName = p.SecondName,
                ThirdName = p.ThirdName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Address = p.Address,
                Phone = p.Phone,
                Email = p.Email,
                CountryID = p.CountryID,
                ImagePath = p.ImagePath,
                UserName = p.UserName
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonResponseDto>> GetPersonById(int id)
        {
            var person = await _peopleService.GetByIdAsync(id);

            if (person == null)
            {
                return NotFound(new { Message = $"Person with ID {id} was not found." });
            }

            var response = new PersonResponseDto
            {
                PersonID = person.PersonID,
                NationalNo = person.NationalNo,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Address = person.Address,
                Phone = person.Phone,
                Email = person.Email,
                CountryID = person.CountryID,
                ImagePath = person.ImagePath,
                UserName = person.UserName
            };

            return Ok(response);
        }

        [HttpGet("national-no/{nationalNo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonResponseDto>> GetPersonByNationalNo(string nationalNo)
        {
            var person = await _peopleService.GetByNationalNoAsync(nationalNo);

            if (person == null)
            {
                return NotFound(new { Message = $"Person with National Number '{nationalNo}' was not found." });
            }

            var response = new PersonResponseDto
            {
                PersonID = person.PersonID,
                NationalNo = person.NationalNo,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Address = person.Address,
                Phone = person.Phone,
                Email = person.Email,
                CountryID = person.CountryID,
                ImagePath = person.ImagePath,
                UserName = person.UserName
            };

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonResponseDto>> CreatePerson([FromBody] CreatePersonDto dto)
        {
            var newPerson = new Person
            {
                NationalNo = dto.NationalNo,
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                ThirdName = dto.ThirdName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email,
                CountryID = dto.CountryID,
                ImagePath = dto.ImagePath,
                UserName = dto.UserName,
                Password = dto.Password
            };

            var isCreated = await _peopleService.CreateAsync(newPerson);

            if (!isCreated)
            {
                return BadRequest(new { Message = "Failed to create the person due to invalid business rules." });
            }

            var responseDto = new PersonResponseDto
            {
                PersonID = newPerson.PersonID,
                NationalNo = newPerson.NationalNo,
                FirstName = newPerson.FirstName,
                SecondName = newPerson.SecondName,
                ThirdName = newPerson.ThirdName,
                LastName = newPerson.LastName,
                DateOfBirth = newPerson.DateOfBirth,
                Gender = newPerson.Gender,
                Address = newPerson.Address,
                Phone = newPerson.Phone,
                Email = newPerson.Email,
                CountryID = newPerson.CountryID,
                ImagePath = newPerson.ImagePath,
                UserName = newPerson.UserName
            };

            return CreatedAtAction(nameof(GetPersonById), new { id = newPerson.PersonID }, responseDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] UpdatePersonDto dto)
        {
            if (id<0)
            {
              return BadRequest(new { Message = "id can not be 0 or lower" });
            }

            var existingPerson = await _peopleService.GetByIdAsync(id);
            if (existingPerson == null)
            {
                return NotFound(new { Message = $"Person with ID {id} was not found." });
            }

            existingPerson.NationalNo = dto.NationalNo;
            existingPerson.FirstName = dto.FirstName;
            existingPerson.SecondName = dto.SecondName;
            existingPerson.ThirdName = dto.ThirdName;
            existingPerson.LastName = dto.LastName;
            existingPerson.DateOfBirth = dto.DateOfBirth;
            existingPerson.Gender = dto.Gender;
            existingPerson.Address = dto.Address;
            existingPerson.Phone = dto.Phone;
            existingPerson.Email = dto.Email;
            existingPerson.CountryID = dto.CountryID;
            existingPerson.ImagePath = dto.ImagePath;
            existingPerson.UserName = dto.UserName;

            var isUpdated = await _peopleService.UpdateAsync(existingPerson);

            if (!isUpdated)
            {
                return BadRequest(new { Message = "An error occurred while updating the person record." });
            }

            return NoContent();
        }

        [HttpPatch("{id}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            var existingPerson = await _peopleService.GetByIdAsync(id);
            if (existingPerson == null)
            {
                return NotFound(new { Message = $"Person with ID {id} was not found." });
            }

            var isUpdated = await _peopleService.UpdatePasswordAsync(id, dto.NewPassword);

            if (!isUpdated)
            {
                return BadRequest(new { Message = "Failed to update the password." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var existingPerson = await _peopleService.GetByIdAsync(id);
            if (existingPerson == null)
            {
                return NotFound(new { Message = $"Person with ID {id} was not found." });
            }

            var isDeleted = await _peopleService.DeleteAsync(id);

            if (!isDeleted)
            {
                return BadRequest(new { Message = "Cannot delete this person due to existing constraints or linked records." });
            }

            return NoContent();
        }
    }
}