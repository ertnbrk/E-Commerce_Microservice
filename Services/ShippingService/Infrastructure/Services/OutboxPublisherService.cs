using Microsoft.Extensions.Hosting;
using Shared.Messaging.Publisher;
using ShippingService.Application.Interfaces;

namespace ShippingService.Infrastructure.Services
{
    public class OutboxPublisherService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxPublisherService> _logger;

        public OutboxPublisherService(IServiceScopeFactory scopeFactory, IServiceProvider serviceProvider, ILogger<OutboxPublisherService> logger)
        {
            _scopeFactory = scopeFactory;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
                var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

                var messages = await repository.GetUnprocessedAsync();
                foreach (var message in messages)
                {
                    try
                    {
                        await publisher.PublishAsync(message.Type, message.Content);
                        message.MarkAsProcessed();
                        await repository.UpdateAsync(message); // 🔴 Burası kritik
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Outbox mesajı gönderilemedi.");
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
