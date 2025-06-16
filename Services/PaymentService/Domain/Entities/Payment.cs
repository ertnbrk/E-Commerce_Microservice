using PaymentService.Domain.Entities.Base;
using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public PaymentStatusEnum Status { get; set; } = PaymentStatusEnum.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime PaymentDate{ get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }

    }
}
