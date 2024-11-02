using InternetShop.Domain.Entities;
using InternetShop.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InternetShop.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InternetShopDbContext _dbContext;

        public ProductRepository(InternetShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Guid id, Product product) =>
            await _dbContext.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.Title, product.Title)
                    .SetProperty(p => p.Description, product.Description)
                    .SetProperty(p => p.Price, product.Price)
                    .SetProperty(p => p.Count, product.Count)
                    .SetProperty(p => p.Brand, product.Brand)
                    .SetProperty(p => p.Category, product.Category));

        public async Task Delete(Guid id) =>
            await _dbContext.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

        public async Task<List<Product>> Get() =>
            await _dbContext.Products
                .AsNoTracking()
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();

        public async Task<List<Product>> GetByFilter(string title, decimal minPrice,decimal maxPrice) =>
            await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Title.ToLower().Contains(title.ToLower()) || (p.Price > minPrice && p.Price < maxPrice))
                        .ToListAsync();

        public Task<Product?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
 
    }
}
