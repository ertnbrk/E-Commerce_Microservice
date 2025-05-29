using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Publisher
{
    public interface IEventPublisher
    {
        Task PublishAsync(string eventType, string content);
    }
}
