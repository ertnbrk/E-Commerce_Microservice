using ShippingService.Application.Interfaces;
using ShippingService.Infrastructure.Data;
using ShippingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShippingService.Infrastructure.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly ShippingDbContext _context;

        public OutboxRepository(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OutboxMessage>> GetUnprocessedAsync()
        {
            // Geçici olarak hepsini döndürüyoruz çünkü IsProcessed kaldırıldı
            return await _context.OutboxMessages.ToListAsync();
        }

        public async Task MarkAsProcessedAsync(Guid messageId)
        {
            // Bu method kullanılmayacaksa tamamen silebilirsin.
            var message = await _context.OutboxMessages.FindAsync(messageId);
            if (message != null)
            {
                // message.IsProcessed = true;
                // message.ProcessedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(OutboxMessage message)
        {
            _context.OutboxMessages.Update(message);
            await _context.SaveChangesAsync();
        }

    }
}
