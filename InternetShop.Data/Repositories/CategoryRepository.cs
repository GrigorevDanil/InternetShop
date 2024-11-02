using InternetShop.Domain.Entities;
using InternetShop.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InternetShopDbContext _dbContext;

        public CategoryRepository(InternetShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> GetById(Guid id) => await _dbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
    }
}
