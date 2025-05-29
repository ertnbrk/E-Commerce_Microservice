using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.UseCases
{
    public class GetAllOrdersUseCase : IGetAllOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> ExecuteAsync()
        {
            return await _orderRepository.GetAllAsync();
        }
    }
}
