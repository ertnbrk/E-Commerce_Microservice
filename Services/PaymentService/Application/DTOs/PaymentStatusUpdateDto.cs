using PaymentService.Domain.Enums;

namespace PaymentService.Application.DTOs
{
    public class PaymentStatusUpdateDto
    {
        public PaymentStatusEnum NewStatus { get; set; }

    }
}
