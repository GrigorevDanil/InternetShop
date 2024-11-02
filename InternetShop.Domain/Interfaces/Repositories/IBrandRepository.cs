using InternetShop.Domain.Entities;

namespace InternetShop.Domain.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        Task<Brand?> GetById(Guid id);
    }
}
