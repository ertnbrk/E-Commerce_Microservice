using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.UseCases
{
    public class GetOrderByIdUseCase : IGetOrderByIdUseCase
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order?> ExecuteAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
