using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IGetAllOrdersUseCase
    {
        Task<List<Order>> ExecuteAsync();

    }
}
