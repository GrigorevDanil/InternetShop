using InternetShop.Domain.Entities;

namespace InternetShop.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> Get();
        Task<User?> GetById(Guid id);
        Task<User?> GetByLogin(string login);
        Task<List<User>> GetByFilter(string fullName, string login, string role);
        Task Add(User user);
        Task Update(Guid id, User user);
        Task Delete(Guid id);
    }
}
