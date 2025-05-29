using OrderService.Infrastructure.Persistence;
using static OrderService.Domain.Enums.OrdersStatus;
using OrderService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Infrastructure.Services
{
    public class OrderUpdateService : IOrderUpdateService
    {
        private readonly OrderDbContext _context;

        public OrderUpdateService(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateStatusAsync(Guid orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            order.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
