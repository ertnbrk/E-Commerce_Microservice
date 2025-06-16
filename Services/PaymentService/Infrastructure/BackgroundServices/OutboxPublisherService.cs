using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging.Publisher;
using PaymentService;
using PaymentService.Infrastructure.Persistence;
namespace PaymentService.Infrastructure.BackgroundServices
{
    public class OutboxPublisherService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxPublisherService> _logger;

        public OutboxPublisherService(IServiceProvider serviceProvider, ILogger<OutboxPublisherService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OutboxPublisherService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
                    var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

                    var messages = await context.OutboxMessages
                        .Where(m => !m.IsPublished)
                        .OrderBy(m => m.PublishedOn)
                        .Take(50)
                        .ToListAsync();

                    _logger.LogInformation("{Count} outbox messages fetched for publishing.", messages.Count);

                    foreach (var msg in messages)
                    {
                        try
                        {
                            _logger.LogInformation("Publishing message ID: {MessageId}, Type: {Type}", msg.Id, msg.Type);

                            await publisher.PublishAsync(msg.Type, msg.Content);

                            msg.IsPublished = true;
                            msg.PublishedOn = DateTime.UtcNow;

                            _logger.LogInformation("Message ID: {MessageId} marked as published.", msg.Id);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error publishing message: {MessageId}", msg.Id);
                        }
                    }

                    await context.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            _logger.LogInformation("OutboxPublisherService stopped.");
        }
    }

}
