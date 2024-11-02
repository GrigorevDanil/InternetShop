using InternetShop.Domain.Entities;
using InternetShop.Domain.Interfaces.Repositories;
using InternetShop.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly WriteDbContext _dbContext;

        public BrandRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Brand?> GetById(Guid id) => await _dbContext.Brands.Include(b => b.Products).FirstOrDefaultAsync(b => b.Id == id);
    }
}