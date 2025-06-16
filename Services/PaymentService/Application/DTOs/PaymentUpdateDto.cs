using PaymentService.Domain.Enums;

namespace PaymentService.Application.DTOs
{
    public class PaymentUpdateDto
    {
        public decimal Amount { get; set; }
        public string? Notes { get; set; } // Optional notes for the payment
        public PaymentStatusEnum NewStatus { get; set; }

    }
}
