using RabbitMQ.Client;
using Shared.Messaging.Publisher;
using System.Text;
using Microsoft.Extensions.Options;

namespace Shared.Messaging.Infrastructure
{
    public class RabbitMqPublisher : IEventPublisher
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqSettings _settings;

        public RabbitMqPublisher(IOptions<RabbitMqSettings> options)
        {
            _settings = options.Value;

            _factory = new ConnectionFactory() { HostName = _settings.HostName };
        }

        public Task PublishAsync(string eventType, string content)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Kuyruk tanımı (idempotent)
            channel.QueueDeclare(
                queue: eventType,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(content);

            // Yayınla
            channel.BasicPublish(
                exchange: "",
                routingKey: eventType,
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }
    }
}
