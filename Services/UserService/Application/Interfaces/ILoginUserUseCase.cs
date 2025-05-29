using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface ILoginUserUseCase
    {
        Task<AuthResponseDto?> ExecuteAsync(LoginDto dto);

    }
}
