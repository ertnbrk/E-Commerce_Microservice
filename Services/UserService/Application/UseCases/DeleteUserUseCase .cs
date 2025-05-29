using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.UseCases
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _repository;

        public DeleteUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return false;

            await _repository.DeleteAsync(user);
            return true;
        }
    }

}
