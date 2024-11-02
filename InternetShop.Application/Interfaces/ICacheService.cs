namespace InternetShop.Application.Interfaces;

public interface ICacheService
{
    Task<T> GetOrCreate<T>(
        string cacheKey,
        Func<Task<T>> factory) where T : class;
}