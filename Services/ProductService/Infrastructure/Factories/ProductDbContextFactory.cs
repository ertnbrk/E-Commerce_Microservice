using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Persistence;

namespace ProductService.Infrastructure.Factories
{
    public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        public ProductDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();

            // Buraya kendi veritabanı bağlantı cümleni yaz (test veya local connection string)
            optionsBuilder.UseSqlServer("Server=localhost;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new ProductDbContext(optionsBuilder.Options);
        }
    }
}
