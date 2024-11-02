using InternetShop.Data.Repositories;
using InternetShop.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio.AspNetCore;
using Microsoft.EntityFrameworkCore;
using InternetShop.Application.Interfaces;
using InternetShop.Domain.Interfaces.Repositories;
using InternetShop.Application.Interfaces.Auth;

namespace InternetShop.Data
{
    public static class Registration
    {
        public static IServiceCollection AddData(
        this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InternetShopDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString(nameof(InternetShopDbContext)), new MySqlServerVersion(new Version(8, 0, 25)));
            });

            services.AddMinio(options =>
            {
                options.Endpoint = "127.0.0.1:9000";
                options.AccessKey = "minio";
                options.SecretKey = "minio123";
            });

            services.AddMemoryCache();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //Services
            services.AddScoped<IMinioService, MinioService>();
            services.AddScoped<ICacheService, CacheService>();


            return services;
        }
    }
}
