using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Core.Models.Entities;
using API.Core.Models;
using API.Repositories;
using API.Core.Models.Responses;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController(GymClubContext context) : ControllerBase
    {
        private readonly UserRepository _repository = new(context);

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repository.GetAll();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.Get(id);

            if (user == null) return NotFound(new Response("Not found."));

            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest(new Response("Invalid ID."));
            }

            try
            {
                await _repository.Put(user, id);
            }
            catch (NullReferenceException)
            {
                return NotFound(new Response("Not found."));
            }

            return Ok(new Response("Change Successful."));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _repository.Delete(id);
            }
            catch (NullReferenceException)
            {
                return NotFound(new Response("Not found."));
            }

            return Ok(new Response("Delete successful."));
        }
    }
}
