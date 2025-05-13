using Microsoft.EntityFrameworkCore;
using ProductService.Models;
using ProductService.Models.Base; 
namespace ProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        // ModifiedAt otomatik güncelleme
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

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                // Tablo adı ve şema
                entity.ToTable("Products", "dbo");

                // Primary key
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId)
                      .HasColumnName("ProductId")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Description)
                      .HasColumnName("Description")
                      .HasMaxLength(500);

                entity.Property(e => e.UnitPrice)
                      .HasColumnName("UnitPrice")
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Stock)
                      .HasColumnName("Stock");

                entity.Property(e => e.Category)
                      .HasColumnName("Category")
                      .HasConversion<string>();

                // BaseEntity'den gelenler
                entity.Property(e => e.CreatedAt)
                      .HasColumnName("CreatedAt");

                entity.Property(e => e.ModifiedAt)
                      .HasColumnName("ModifiedAt");

                entity.Property(e => e.IsActive)
                      .HasColumnName("IsActive");

                // NotMapped alan: DisplayInfo → EF tarafından göz ardı edilir
                entity.Ignore(e => e.DisplayInfo);
            });
        }
    }
}
