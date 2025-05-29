using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using static OrderService.Domain.Enums.OrdersStatus;

namespace OrderService.Infrastructure.Services
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly OrderDbContext _context;

        public OrderQueryService(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
        {
            if (!Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
                return Enumerable.Empty<Order>();

            return await _context.Orders
                .Where(o => o.Status == parsedStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByProductIdAsync(Guid productId)
        {
            return await _context.Orders
                .Where(o => o.ProductId == productId)
                .ToListAsync();
        }
    }
}
