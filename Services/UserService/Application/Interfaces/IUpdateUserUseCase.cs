using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IUpdateUserUseCase
    {
        Task<bool> ExecuteAsync(UpdateUserDto dto);

    }
}
