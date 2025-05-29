using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.UseCases
{
    public class GetOrdersByUserIdUseCase : IGetOrdersByUserIdUseCase
    {
        private readonly IOrderRepository _repository;

        public GetOrdersByUserIdUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Order>> ExecuteAsync(Guid userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }
    }
}
