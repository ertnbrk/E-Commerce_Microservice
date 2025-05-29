using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.UseCases
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginUserUseCase(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<AuthResponseDto?> ExecuteAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                return null;

            var credentials = await _userRepository.GetCredentialsByUserIdAsync(user.UserId);
            if (credentials == null)
                return null;

            bool isValid = _authService.VerifyPassword(credentials.HashedPassword, dto.Password);
            if (!isValid)
                return null;

            var token = _authService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }
    }

}
