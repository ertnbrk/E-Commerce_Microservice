namespace OrderService.Application.DTOs
{
    public class OrderCreateDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
