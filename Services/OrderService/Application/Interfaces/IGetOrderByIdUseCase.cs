using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IGetOrderByIdUseCase
    {
        Task<Order?> ExecuteAsync(Guid id);

    }
}
