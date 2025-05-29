using UserService.Domain.Enums;

namespace UserService.Application.DTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly BirthDate { get; set; }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string PhoneCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
