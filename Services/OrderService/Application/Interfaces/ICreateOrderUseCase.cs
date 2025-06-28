using OrderService.Application.DTOs;
using OrderService.Domain.Entities;
namespace OrderService.Application.Interfaces
{
    public interface ICreateOrderUseCase
    {
        Task<Order> ExecuteAsync(OrderCreateDto dto, Guid userId,Guid productId);

    }
}
