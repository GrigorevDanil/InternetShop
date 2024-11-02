using InternetShop.Domain.Entities;

namespace InternetShop.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetById(Guid id);
    }
}
