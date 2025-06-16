using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Messaging.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Infrastructure
{
    public class QueueInitializer
    {
        private readonly RabbitMqSettings _settings;
        private readonly ILogger<QueueInitializer> _logger;

        public QueueInitializer(IOptions<RabbitMqSettings> options, ILogger<QueueInitializer> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public void EnsureQueuesExist()
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password
            };
            const int maxRetries = 10;
            int attempt = 0;

            while (attempt < maxRetries) {
                try
                {
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    string[] queues = new[]
                    {
                "OrderCreatedEvent",
                "OrderCreatedEvent_error"
            };

                    foreach (var queue in queues)
                    {
                        channel.QueueDeclare(
                            queue: queue,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        _logger.LogInformation("Declared queue: {Queue}", queue);
                    }
                    break; // if it succeeds, exit the loop
                }
                catch (Exception ex)
                {
                    attempt++;
                    _logger.LogWarning(ex, "RabbitMQ bağlantısı başarısız. {Attempt}/{Max} tekrar denenecek...", attempt, maxRetries);
                    Thread.Sleep(3000);
                    
                }
            }
            if (attempt == maxRetries)
            {
                throw new Exception("RabbitMQ'ya bağlanılamadı. Maksimum deneme sayısına ulaşıldı.");
            }

        }
    }
}
