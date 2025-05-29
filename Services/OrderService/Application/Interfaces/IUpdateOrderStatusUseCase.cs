using OrderService.Application.DTOs;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IUpdateOrderStatusUseCase
    {
        Task<bool> ExecuteAsync(Guid orderId, OrderStatusUpdateDto dto);

    }
}
