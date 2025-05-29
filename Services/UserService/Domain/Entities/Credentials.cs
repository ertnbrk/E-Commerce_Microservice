using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entities
{
    [Table("Credentials", Schema = "auth")]
    public class Credentials
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public string HashedPassword { get; set; } = null!;

        public virtual Users User { get; set; } = null!;
    }
}
