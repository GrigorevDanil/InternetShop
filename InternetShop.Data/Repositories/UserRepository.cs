using InternetShop.Domain.Entities;
using InternetShop.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InternetShop.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InternetShopDbContext _dbContext;

        public UserRepository(InternetShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Guid id, User user) =>
            await _dbContext.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(p => p.FullName, user.FullName)
                    .SetProperty(p => p.Gender, user.Gender)
                    .SetProperty(p => p.PhoneNumber, user.PhoneNumber)
                    .SetProperty(p => p.Credentials, user.Credentials)
                    .SetProperty(p => p.Role, user.Role)
                    .SetProperty(p => p.Email, user.Email)
        );


        public async Task Delete(Guid id) =>
            await _dbContext.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

        public async Task<List<User>> Get() =>
            await _dbContext.Users
                .AsNoTracking()
                .OrderBy(u => u.FullName.Surname)
                .ToListAsync();

        public async Task<List<User>> GetByFilter(string fullName, string login, string role) =>
            await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.FullName.ToString() == fullName || u.Credentials.Login == login || u.Role.Value == role)
                .OrderBy(u => u.FullName.Surname)
                .ToListAsync();

        public async Task<User?> GetById(Guid id) =>
            await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

        public async Task<User?> GetByLogin(string login) =>
            await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Credentials.Login == login)
                .FirstOrDefaultAsync();
    }
}
