using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("ModifiedAt")]
        public DateTime? ModifiedAt { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; } = true;
    }
}
