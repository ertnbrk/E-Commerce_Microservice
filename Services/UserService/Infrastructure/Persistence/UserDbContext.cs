using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Credentials> Credentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserRole enum'unu string olarak sakla
            modelBuilder.Entity<Users>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Credentials>().ToTable("Credentials", "auth");

            modelBuilder.Entity<Credentials>()
                .HasOne(c => c.User)
                .WithOne(u => u.Credentials)
                .HasForeignKey<Credentials>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
