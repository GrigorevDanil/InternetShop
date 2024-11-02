using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using InternetShop.Application.Database;
using InternetShop.Infrastructure.DbContexts;
using InternetShop.Infrastructure.Options;
using InternetShop.Application.Interfaces;
using InternetShop.Infrastructure.Services;
using InternetShop.Domain.Interfaces.Repositories;
using InternetShop.Infrastructure.Repositories;
using InternetShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.Infrastructure
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMemoryCache()
                .AddDbContexts()
                .AddServices()
                .AddRepositories()
                .AddMinio(configuration);

            return services;
        }

        private static IServiceCollection AddDbContexts(this IServiceCollection services)
        {
            services.AddScoped<WriteDbContext>();
            services.AddScoped<IReadDbContext, ReadDbContext>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMinioService, MinioService>();
            services.AddScoped<ICacheService, CacheService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MinioOptions>(
            configuration.GetSection(MinioOptions.MINIO));

            services.AddMinio(options =>
            {
                var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                               ?? throw new ApplicationException("Missing minio configuration");

                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
                options.WithSSL(minioOptions.WithSsl);
            });

            return services;
        }
    }
}
