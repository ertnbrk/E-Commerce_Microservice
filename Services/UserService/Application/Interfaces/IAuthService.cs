using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(Users user);
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string inputPassword);
    }
}
