﻿using InternetShop.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace InternetShop.Data.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrCreate<T>(
            string cacheKey,
            Func<Task<T>> factory) where T : class
        {
            var cacheExists = _cache.TryGetValue(cacheKey, out T? value);
            if (cacheExists && value is not null)
            {
                return value;
            }

            value = await factory();
            _cache.Set(cacheKey, value, TimeSpan.FromMinutes(2));

            return value;
        }
    }
}
