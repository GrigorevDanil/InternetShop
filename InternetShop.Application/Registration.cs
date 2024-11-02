using InternetShop.Application.Interfaces.Auth;
using InternetShop.Application.Products.Commands;
using InternetShop.Application.Products.Queries;
using InternetShop.Application.Users.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace InternetShop.Application
{
    public static class Registration
    {
        public static IServiceCollection AddApplication(
        this IServiceCollection services)
        {
            //Products
            services.AddScoped<PublishProductHandler>();
            services.AddScoped<GetProductsHandler>();

            //Users
            services.AddScoped<RegisterUserHandler>();
            services.AddScoped<LoginUserHandler>();


            return services;
        }
    }
}
