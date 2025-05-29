using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Application.UseCases
{
    public class GetOrdersByStatusUseCase : IGetOrdersByStatusUseCase
    {
        private readonly IOrderRepository _repository;

        public GetOrdersByStatusUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Order>> ExecuteAsync(OrderStatus status)
        {
            return await _repository.GetByStatusAsync(status);
        }
    }
}
