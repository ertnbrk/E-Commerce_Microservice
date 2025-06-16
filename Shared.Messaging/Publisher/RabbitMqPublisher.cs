using RabbitMQ.Client;
using Shared.Messaging.Publisher;
using System.Text;
using Microsoft.Extensions.Options;
using Shared.Messaging.Configuration;
using Microsoft.Extensions.Logging;

namespace Shared.Messaging.Infrastructure
{
    public class RabbitMqPublisher : IEventPublisher
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqSettings _settings;
        private readonly ILogger<RabbitMqPublisher> _logger;
        private readonly bool _forceErrorInPublish = false;

        public RabbitMqPublisher(IOptions<RabbitMqSettings> options, ILogger<RabbitMqPublisher> logger)
        {
            _settings = options.Value;
            _logger = logger;

            _factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password
            };
        }

        public Task PublishAsync(string eventType, string content)
        {
            try
            {
                // TEST: Hata tetikleme (forceErrorInPublish) için kullanılır. mevcut durum false
                if (_forceErrorInPublish)
                {
                    throw new Exception("Test amaçlı hata.");
                }
                
                using var connection = _factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: eventType, durable: true, exclusive: false, autoDelete: false);

                var body = Encoding.UTF8.GetBytes(content);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "", routingKey: eventType, basicProperties: properties, body: body);

                _logger.LogInformation("Published to queue: {Queue}, Content: {Content}", eventType, content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing to queue: {Queue}. Writing to error queue...", eventType);

                try
                {
                    using var connection = _factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    string errorQueue = eventType + "_error";
                    channel.QueueDeclare(queue: errorQueue, durable: true, exclusive: false, autoDelete: false);

                    var body = Encoding.UTF8.GetBytes(content);

                    channel.BasicPublish(exchange: "", routingKey: errorQueue, basicProperties: null, body: body);

                    _logger.LogInformation("Published to ERROR queue: {Queue}, Content: {Content}", errorQueue, content);
                }
                catch (Exception innerEx)
                {
                    _logger.LogCritical(innerEx, "Failed to publish to ERROR queue.");
                }
            }

            return Task.CompletedTask;
        }
    }

}
