using RabbitMQ.Client;
using Shared.Messaging.Publisher;
using System.Text;
using Microsoft.Extensions.Options;
using Shared.Messaging.Configuration;

namespace Shared.Messaging.Infrastructure
{
    public class RabbitMqPublisher : IEventPublisher
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqSettings _settings;

        public RabbitMqPublisher(IOptions<RabbitMqSettings> options)
        {
            _settings = options.Value;

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
            Console.WriteLine($"Publishing event to RabbitMQ. Queue: {eventType}, Content: {content}");

            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: eventType,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(content);

            channel.BasicPublish(
                exchange: "",
                routingKey: eventType,
                basicProperties: null,
                body: body);

            Console.WriteLine($"Event published successfully to queue '{eventType}'");

            return Task.CompletedTask;
        }
    }

}
