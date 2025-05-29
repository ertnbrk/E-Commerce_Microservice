using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Application.DTOs
{
    public class OrderStatusUpdateDto
    {
        public OrderStatus Status { get; set; }

    }
}
