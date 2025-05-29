using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.UseCases
{
    public class DeleteOrderUseCase : IDeleteOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid orderId)
        {
            var order = await _repository.GetByIdAsync(orderId);
            if (order == null)
                return false;

            await _repository.DeleteAsync(orderId);
            return true;
        }
    }
}
