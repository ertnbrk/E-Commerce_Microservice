using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Entities.Base;

namespace PaymentService.Infrastructure.Persistence
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
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

            modelBuilder.Entity<OutboxMessage>()
                        .HasIndex(x => x.IsPublished);

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments", "dbo");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("PaymentId")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                      .HasColumnName("UserId")
                      .IsRequired();

                entity.Property(e => e.OrderId)
                      .HasColumnName("OrderId")
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnName("Amount")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.PaymentDate)
                      .HasColumnName("PaymentDate")
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
