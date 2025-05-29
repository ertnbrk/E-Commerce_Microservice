using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IOrderQueryService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetByStatusAsync(string status);
        Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Order>> GetByProductIdAsync(Guid productId);
    }
}
