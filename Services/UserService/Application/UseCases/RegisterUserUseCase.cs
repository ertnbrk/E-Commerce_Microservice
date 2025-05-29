using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.UseCases
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> ExecuteAsync(RegisterDto dto)
        {
            var user = new Users
            {
                UserId = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Email = dto.Email,
                PhoneCode = dto.PhoneCode,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                Role = dto.Role,
                IsActive = true,
                PhoneVerified = false,
                CreatedAt = DateTime.UtcNow,
                VerifiedAt = DateTime.UtcNow
            };

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var credentials = new Credentials
            {
                UserId = user.UserId,
                HashedPassword = hashedPassword
            };

            await _userRepository.CreateWithCredentialsAsync(user, credentials);
            return user.UserId;
        }
    }
}
