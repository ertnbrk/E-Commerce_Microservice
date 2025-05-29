using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IDeleteUserUseCase
    {
        Task<bool> ExecuteAsync(Guid id);

    }
}
