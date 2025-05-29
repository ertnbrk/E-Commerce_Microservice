using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IOutboxRepository
    {
        Task AddAsync(OutboxMessage message);
        Task<List<OutboxMessage>> GetUnprocessedAsync();
        Task MarkAsProcessedAsync(Guid id);
    }
}
