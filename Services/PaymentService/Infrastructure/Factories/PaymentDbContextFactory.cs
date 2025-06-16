using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using PaymentService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Factories
{
    public class PaymentDbContextFactory : IDesignTimeDbContextFactory<PaymentDbContext>
    {
        public PaymentDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PaymentDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new PaymentDbContext(optionsBuilder.Options);
        }
    }
}
