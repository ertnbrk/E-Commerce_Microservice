namespace UserService.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; } // Entity'de UserId olarak geçiyor
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool PhoneVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime VerifiedAt { get; set; }
    }
}
