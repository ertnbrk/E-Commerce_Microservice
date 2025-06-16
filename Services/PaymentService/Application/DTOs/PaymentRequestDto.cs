namespace PaymentService.Application.DTOs
{
    public class PaymentRequestDto
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
