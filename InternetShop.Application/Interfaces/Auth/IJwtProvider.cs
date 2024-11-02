using InternetShop.Domain.Entities;

namespace InternetShop.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}