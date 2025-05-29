using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.UseCases
{
    public class GetOrdersByProductIdUseCase : IGetOrdersByProductIdUseCase
    {
        private readonly IOrderRepository _repository;

        public GetOrdersByProductIdUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Order>> ExecuteAsync(Guid productId)
        {
            return await _repository.GetByProductIdAsync(productId);
        }
    }
}
