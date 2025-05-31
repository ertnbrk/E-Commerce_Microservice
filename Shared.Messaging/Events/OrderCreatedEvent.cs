using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Shared.Messaging.Events
{
    public class OrderCreatedEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public DateTime CreatedAt   { get; set; } = DateTime.UtcNow;

    }
}
