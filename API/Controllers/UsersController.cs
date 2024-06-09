using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Core.Models.Entities;
using API.Core.Interfaces;
using API.Core.Dto;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController(IService<UserDto> service) : ControllerBase
    {
        private readonly IService<UserDto> _service = service;

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _service.GetAll();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            UserDto? user;

            try
            {
                user = await _service.Get(id);
            }
            catch (NullReferenceException)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto user)
        {
            if (id != user.UserId)
            {
                return BadRequest("Invalid ID.");
            }

            try
            {
                await _service.Put(id, user);
            }
            catch (NullReferenceException)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _service.Delete(id);
            }
            catch (NullReferenceException)
            {
                return NotFound("User not found.");
            }

            return Ok();
        }
    }
}
