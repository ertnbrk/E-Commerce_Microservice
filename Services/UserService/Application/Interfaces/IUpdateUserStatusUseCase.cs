using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IUpdateUserStatusUseCase
    {
        Task<bool> ExecuteAsync(Guid id, UpdateUserStatusDto dto);

    }
}
