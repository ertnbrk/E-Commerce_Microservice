using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Application.UseCases
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public CreateOrderUseCase(
            IOrderRepository orderRepository,
            IProductService productService,
            IUserService userService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _userService = userService;
        }

        public async Task<Order> ExecuteAsync(OrderCreateDto dto, Guid userId)
        {
            //Validate user (Optional)
            var userExists = await _userService.UserExistsAsync(userId);
            if (!userExists)
                throw new Exception("Kullanıcı bulunamadı.");

            //Get Product Information
            var product = await _productService.GetProductByIdAsync(dto.ProductId);
            if (product == null || !product.IsActive)
                throw new Exception("Ürün bulunamadı veya aktif değil.");

            //Create Order
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = product.UnitPrice,
                TotalPrice = dto.Quantity * product.UnitPrice,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Notes = dto.Notes,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            
            await _orderRepository.AddOrderAsync(order);

            return order;
        }
    }
}
