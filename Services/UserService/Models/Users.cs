using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    [Table("Users", Schema = "dbo")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [Column("BirthDate")]
        public DateOnly BirthDate { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("HashedPassword")]
        public string HashedPassword { get; set; }

        [Column("PhoneCode")]
        public string PhoneCode { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("PhoneVerified")]
        public bool PhoneVerified { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("VerifiedAt")]
        public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;
    }
    public class UpdateUserStatusDto
    {
        public bool IsActive { get; set; }
        public bool PhoneVerified { get; set; }
    }
}
