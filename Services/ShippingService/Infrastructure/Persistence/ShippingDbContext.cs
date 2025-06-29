using Microsoft.EntityFrameworkCore;
using ShippingService.Domain.Entities;

namespace ShippingService.Infrastructure.Data;

public class ShippingDbContext : DbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<Shipment> Shipments => Set<Shipment>();

    public ShippingDbContext(DbContextOptions<ShippingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        base.OnModelCreating(modelBuilder);
    }
}
