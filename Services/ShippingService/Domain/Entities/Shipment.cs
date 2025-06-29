
using ShippingService.Domain.Enums;

namespace ShippingService.Domain.Entities;

public class Shipment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public DateTime ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public string Address { get; set; }
    public DateTime ModifiedAt { get; set; }

    public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;
}
