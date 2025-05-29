using OrderService.Domain.Entities.Base;
using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }     // Ürün fiyatı sipariş anında
        public decimal TotalPrice { get; set; }    // Hesaplanmış değer (Quantity * UnitPrice)

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string? Notes { get; set; }         // Admin notu, müşteri notu vs.
    }
    

  
}
