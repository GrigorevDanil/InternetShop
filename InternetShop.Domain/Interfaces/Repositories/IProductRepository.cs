using InternetShop.Domain.Entities;

namespace InternetShop.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> Get();
        Task<Product?> GetById(Guid id);
        Task<List<Product>> GetByFilter(string title, decimal minPrice, decimal maxPrice);
        Task Add(Product product);
        Task Update(Guid id, Product product);
        Task Delete(Guid id);
    }
}
