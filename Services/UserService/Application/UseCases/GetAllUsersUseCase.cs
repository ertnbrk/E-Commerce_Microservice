using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.UseCases
{
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private readonly IUserRepository _repository;

        public GetAllUsersUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserDto>> ExecuteAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role.ToString(),
                IsActive = u.IsActive,
                PhoneVerified = u.PhoneVerified
            }).ToList();
        }
    }
}
