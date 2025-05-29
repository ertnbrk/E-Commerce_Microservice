using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.UseCases
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public CreateUserUseCase(IUserRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
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

            var hashedPassword = _authService.HashPassword(dto.Password);

            var credentials = new Credentials
            {
                UserId = user.UserId,
                HashedPassword = hashedPassword
            };

            await _repository.CreateWithCredentialsAsync(user, credentials);

            return user.UserId;
        }
    }

}
