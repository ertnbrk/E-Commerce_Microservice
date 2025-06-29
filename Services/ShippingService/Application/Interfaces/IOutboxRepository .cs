using ShippingService.Domain.Entities;

namespace ShippingService.Application.Interfaces
{
    public interface IOutboxRepository
    {
        Task AddAsync(OutboxMessage message);
        Task<List<OutboxMessage>> GetUnprocessedAsync();
        Task MarkAsProcessedAsync(Guid id);
        Task UpdateAsync(OutboxMessage message);


    }
}
