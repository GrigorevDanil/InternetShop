using InternetShop.Domain.Entities;
using InternetShop.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly InternetShopDbContext _dbContext;

        public BrandRepository(InternetShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Brand?> GetById(Guid id) => await _dbContext.Brands.Include(b => b.Products).FirstOrDefaultAsync(b => b.Id == id);
    }
}
