using OrderService.Application.DTOs;

namespace OrderService.Application.Interfaces
{
    public interface IDeleteOrderUseCase
    {
        Task<bool> ExecuteAsync(Guid orderId);

    }
}
