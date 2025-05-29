using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IGetOrdersByUserIdUseCase
    {
        Task<List<Order>> ExecuteAsync(Guid userId);

    }
}
