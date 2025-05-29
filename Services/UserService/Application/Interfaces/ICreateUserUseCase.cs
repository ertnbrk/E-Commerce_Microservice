using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface ICreateUserUseCase
    {
        Task<Guid> ExecuteAsync(RegisterDto dto);

    }
}
