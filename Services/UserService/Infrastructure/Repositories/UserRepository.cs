using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task CreateWithCredentialsAsync(Users user, Credentials credentials)
        {
            await _context.Users.AddAsync(user);
            await _context.Credentials.AddAsync(credentials);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<Users?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Credentials?> GetCredentialsByUserIdAsync(Guid userId)
        {
            return await _context.Credentials.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        // 🔧 Eksik metotlar:

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Users user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
