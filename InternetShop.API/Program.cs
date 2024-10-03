using InternetShop.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InternetShopDbContext>(option =>
{
    option.UseMySql(builder.Configuration.GetConnectionString(nameof(InternetShopDbContext)), new MySqlServerVersion(new Version(8, 0, 25)));
});

var app = builder.Build();



app.Run();
