using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IGetOrdersByProductIdUseCase
    {
        Task<List<Order>> ExecuteAsync(Guid productId);

    }
}
