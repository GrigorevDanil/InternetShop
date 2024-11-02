using InternetShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InternetShop.Infrastructure.DbContexts
{
    public class WriteDbContext(IConfiguration configuration) : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(configuration.GetConnectionString(Constants.DATABASE), new MySqlServerVersion(new Version(8, 0, 25)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Write") ?? false);

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserToken<Guid>>()
                .ToTable("UserTokens");

            modelBuilder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("UserLogins");

            modelBuilder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("RoleClaims");

            modelBuilder.Entity<IdentityUserRole<Guid>>()
                .ToTable("UserRoles");
        }
    }
}
