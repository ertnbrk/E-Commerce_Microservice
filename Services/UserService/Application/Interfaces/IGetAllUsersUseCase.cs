using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IGetAllUsersUseCase
    {
        Task<List<UserDto>> ExecuteAsync();

    }
}
