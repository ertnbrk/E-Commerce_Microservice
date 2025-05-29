using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGetAllUsersUseCase _getAllUsers;
        private readonly IGetUserByIdUseCase _getUserById;
        private readonly IUpdateUserUseCase _updateUser;
        private readonly IDeleteUserUseCase _deleteUser;
        private readonly IUpdateUserStatusUseCase _updateUserStatus;

        public UsersController(
            IGetAllUsersUseCase getAllUsers,
            IGetUserByIdUseCase getUserById,
            IUpdateUserUseCase updateUser,
            IDeleteUserUseCase deleteUser,
            IUpdateUserStatusUseCase updateUserStatus)
        {
            _getAllUsers = getAllUsers;
            _getUserById = getUserById;
            _updateUser = updateUser;
            _deleteUser = deleteUser;
            _updateUserStatus = updateUserStatus;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _getAllUsers.ExecuteAsync();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _getUserById.ExecuteAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            var success = await _updateUser.ExecuteAsync(dto);
            return success ? NoContent() : NotFound();
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateUserStatusDto dto)
        {
            var success = await _updateUserStatus.ExecuteAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _deleteUser.ExecuteAsync(id);
            return success ? NoContent() : NotFound();
        }
        [HttpGet("throw")]
        public IActionResult ThrowTestException()
        {
            throw new Exception("Test exception from UserService.");
        }

    }
}
