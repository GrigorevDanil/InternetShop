using InternetShop.Application.Database;
using InternetShop.Application.Dtos;
using InternetShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InternetShop.Infrastructure.DbContexts
{
    public class ReadDbContext(IConfiguration configuration) : IdentityDbContext<User, Role, Guid>, IReadDbContext
    {
        public IQueryable<ProductDTO> Products => Set<ProductDTO>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(configuration.GetConnectionString(Constants.DATABASE), new MySqlServerVersion(new Version(8, 0, 25)));
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Read") ?? false);

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
