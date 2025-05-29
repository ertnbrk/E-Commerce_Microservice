using OrderService.Domain.Entities;
using static OrderService.Domain.Enums.OrdersStatus;
namespace OrderService.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetByUserIdAsync(Guid userId);
        Task<List<Order>> GetByProductIdAsync(Guid productId);
        Task<List<Order>> GetByStatusAsync(OrderStatus status);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
}
