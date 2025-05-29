using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IGetUserByIdUseCase
    {
        Task<UserDto?> ExecuteAsync(Guid id);

    }
}
