using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserControllers(UserDbContext userDbContext)
        {
            this._context = userDbContext;
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<Users>> GetUsers()
        {
            return _context.Users;
        }
        [HttpGet("{UserId:guid}")]
        public  async Task<ActionResult<Users>> GetUserById(Guid UserId)
        {
            var User = await _context.Users.FindAsync(UserId);
            return User;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Users users)
        {
            await _context.Users.AddAsync(users);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Users users)
        {
            _context.Users.Update(users);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateUserStatusDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.IsActive = dto.IsActive;
            user.PhoneVerified = dto.PhoneVerified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{UserId:guid}")]
        public async Task<ActionResult> Delete(Guid UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
