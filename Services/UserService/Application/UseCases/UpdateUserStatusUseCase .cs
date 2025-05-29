using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.UseCases
{
    public class UpdateUserStatusUseCase : IUpdateUserStatusUseCase
    {
        private readonly IUserRepository _repository;

        public UpdateUserStatusUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid id, UpdateUserStatusDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return false;

            user.IsActive = dto.IsActive;
            user.PhoneVerified = dto.PhoneVerified;

            await _repository.UpdateAsync(user);
            return true;
        }
    }

}
