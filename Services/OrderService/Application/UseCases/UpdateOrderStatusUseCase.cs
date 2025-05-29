using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Application.UseCases
{
    public class UpdateOrderStatusUseCase : IUpdateOrderStatusUseCase
    {
        private readonly IOrderRepository _repository;

        public UpdateOrderStatusUseCase(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid orderId, OrderStatusUpdateDto dto)
        {
            var order = await _repository.GetByIdAsync(orderId);
            if (order == null || !Enum.IsDefined(typeof(OrderStatus), dto.Status))
                return false;

            order.Status = dto.Status;
            await _repository.UpdateAsync(order);
            return true;
        }
    }
}
