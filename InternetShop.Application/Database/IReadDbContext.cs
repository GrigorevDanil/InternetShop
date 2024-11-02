using InternetShop.Application.Dtos;

namespace InternetShop.Application.Database
{
    public interface IReadDbContext
    {
        IQueryable<ProductDTO> Products { get; }
    }
}
