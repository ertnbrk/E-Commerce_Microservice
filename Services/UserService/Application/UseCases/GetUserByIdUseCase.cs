using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.UseCases
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly IUserRepository _repository;

        public GetUserByIdUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto?> ExecuteAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),
                IsActive = user.IsActive,
                PhoneVerified = user.PhoneVerified
            };
        }
    }
}
