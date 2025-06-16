namespace PaymentService.Application.DTOs
{
    public class PaymentCreateDto
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } // e.g., CreditCard, PayPal, etc.
        public string? Notes { get; set; } // Optional notes for the payment

    }
}
