using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Services;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterUserUseCase _registerUseCase;
        private readonly ILoginUserUseCase _loginUseCase;

        public AuthController(
            IRegisterUserUseCase registerUseCase,
            ILoginUserUseCase loginUseCase)
        {
            _registerUseCase = registerUseCase;
            _loginUseCase = loginUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _registerUseCase.ExecuteAsync(dto);
            if (result == Guid.Empty)
                return BadRequest("Kullanıcı oluşturulamadı.");

            return Ok(new { Message = "Kayıt başarılı", UserId = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var authResult = await _loginUseCase.ExecuteAsync(dto);
            if (authResult == null)
                return Unauthorized("Giriş başarısız.");

            return Ok(authResult);
        }
    }
}
