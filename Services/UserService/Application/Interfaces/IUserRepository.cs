using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Users?> GetByEmailAsync(string email);
        Task AddAsync(Users user);
        Task<List<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(Guid id);
        Task CreateAsync(Users user);
        Task CreateWithCredentialsAsync(Users user, Credentials credentials);
        Task<Credentials?> GetCredentialsByUserIdAsync(Guid userId);
        Task UpdateAsync(Users user);
        Task DeleteAsync(Users user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
