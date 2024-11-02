using InternetShop.Domain.Entities;
using InternetShop.Domain.Interfaces.Repositories;
using InternetShop.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WriteDbContext _dbContext;

        public CategoryRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> GetById(Guid id) => await _dbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
    }
}