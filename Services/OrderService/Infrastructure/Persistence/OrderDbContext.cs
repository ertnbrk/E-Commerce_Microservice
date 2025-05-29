using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Entities.Base;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrderService.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public override int SaveChanges()
        {
            UpdateModifiedAt();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateModifiedAt();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateModifiedAt()
        {
            var modifiedEntries = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                entry.Entity.ModifiedAt = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders", "dbo");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("OrderId")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                      .HasColumnName("UserId")
                      .IsRequired();

                entity.Property(e => e.ProductId)
                      .HasColumnName("ProductId")
                      .IsRequired();

                entity.Property(e => e.Quantity)
                      .HasColumnName("Quantity")
                      .IsRequired();

                entity.Property(e => e.UnitPrice)
                      .HasColumnName("UnitPrice")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.TotalPrice)
                      .HasColumnName("TotalPrice")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.OrderDate)
                      .HasColumnName("OrderDate")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnName("Status")
                      .HasConversion<string>()
                      .IsRequired();

                entity.Property(e => e.Notes)
                      .HasColumnName("Notes")
                      .HasMaxLength(1000);

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("CreatedAt");

                entity.Property(e => e.ModifiedAt)
                      .HasColumnName("ModifiedAt");

                entity.Property(e => e.IsActive)
                      .HasColumnName("IsActive");
            });
        }
    }
}
