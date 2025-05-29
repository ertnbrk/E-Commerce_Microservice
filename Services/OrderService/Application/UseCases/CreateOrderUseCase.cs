using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories;
using Shared.Messaging.Events;
using System.Text.Json;
using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Application.UseCases
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IOutboxRepository _outboxRepository;

        public CreateOrderUseCase(
            IOrderRepository orderRepository,
            IProductService productService,
            IUserService userService,
            IOutboxRepository outboxRepository
            )
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _userService = userService;            _outboxRepository = outboxRepository;

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


            var outboxMessage = new OutboxMessage
            {
                Type = nameof(OrderCreatedEvent),
                Content = JsonSerializer.Serialize(new OrderCreatedEvent
                {
                    OrderId = order.Id,
                    ProductId = order.ProductId,
                    Quantity = order.Quantity,
                    CreatedAt = order.CreatedAt
                })
            };

            await _outboxRepository.AddAsync(outboxMessage);


            return order;
        }


    }
}
