using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Application.Interfaces
{
    public interface IGetOrdersByStatusUseCase
    {
        Task<List<Order>> ExecuteAsync(OrderStatus status);

    }
}
