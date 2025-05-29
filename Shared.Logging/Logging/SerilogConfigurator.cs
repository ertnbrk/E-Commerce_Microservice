using Serilog;
using Serilog.Events;

namespace Shared.Logging.Logging
{
    public static class SerilogConfigurator
    {
        public static void Configure(string serviceName)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Tüm seviyeler aktiftir
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.WithProperty("Service", serviceName)
                .Enrich.FromLogContext()

                // Konsola tüm seviyeleri yaz
                .WriteTo.Console()

                // Günlük dosyasına seviyelere göre yaz
                .WriteTo.File(
                    path: $"logs/{serviceName}/debug-.log",
                    restrictedToMinimumLevel: LogEventLevel.Debug,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    path: $"logs/{serviceName}/warning-.log",
                    restrictedToMinimumLevel: LogEventLevel.Warning,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    path: $"logs/{serviceName}/error-.log",
                    restrictedToMinimumLevel: LogEventLevel.Error,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }
    }
}
