namespace ShippingService.Application.DTOs
{
    public class CreateShipmentDto
    {
        public Guid OrderId { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime EstimatedDeliveryDate { get; set; }  // İsteğe bağlı
        public string RecipientName { get; set; } = string.Empty;

    }
}
