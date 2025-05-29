using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly OrderDbContext _context;

        public OutboxRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxMessage message)
        {
            await _context.OutboxMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OutboxMessage>> GetUnprocessedAsync()
        {
            return await _context.OutboxMessages
                .Where(m => m.ProcessedAt == null)
                .ToListAsync();
        }

        public async Task MarkAsProcessedAsync(Guid id)
        {
            var msg = await _context.OutboxMessages.FindAsync(id);
            if (msg != null)
            {
                msg.ProcessedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }

}
