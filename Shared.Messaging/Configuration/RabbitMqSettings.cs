using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Configuration
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; } = string.Empty;
        public string UserName { get; set; } = "admin";
        public string Password { get; set; } = "admin";
        public int Port { get; set; } = 5672;

    }
}
