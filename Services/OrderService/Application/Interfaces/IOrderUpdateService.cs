using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Application.Interfaces
{
    public interface IOrderUpdateService
    {
        Task<bool> UpdateStatusAsync(Guid orderId, OrderStatus newStatus);
        Task<bool> DeleteAsync(Guid orderId);
    }
}
