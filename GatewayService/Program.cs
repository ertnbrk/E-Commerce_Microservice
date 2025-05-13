using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewayService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(80); // 🔥 Sadece HTTP portunu kullan
            });

            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot();

            var app = builder.Build();

            app.UseRouting();
            await app.UseOcelot();
            app.Run();
        }
    }
}
