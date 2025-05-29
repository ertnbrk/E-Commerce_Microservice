using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.UseCases
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserRepository _repository;

        public UpdateUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(UpdateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(dto.Id);
            if (user == null)
                return false;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Address = dto.Address;

            await _repository.UpdateAsync(user);
            return true;
        }
    }

}
