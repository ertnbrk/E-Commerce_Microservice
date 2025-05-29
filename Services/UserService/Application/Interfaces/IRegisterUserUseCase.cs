using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IRegisterUserUseCase
    {
        Task<Guid> ExecuteAsync(RegisterDto dto);

    }
}
