using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace ProductService.Infrastructure.Factories
{
    public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        public ProductDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new ProductDbContext(optionsBuilder.Options);
        }
    }
}
