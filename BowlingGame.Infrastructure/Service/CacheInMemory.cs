using BowlingGame.Application.Service;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BowlingGame.Infrastructure.Service
{
    public class CacheInMemory<TEntity> : ICache<TEntity>
    {
        private readonly IMemoryCache _memoryCache;

        public CacheInMemory(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add(string key, TEntity entity, TimeSpan timeSpan)
        {
            var settings = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(timeSpan.TotalMinutes)
            };
            
            _memoryCache.Set(key, entity, settings);
        }

        public bool TryGet(string key, out TEntity data)
        {
            return _memoryCache.TryGetValue(key, out data);
        }
    }
}
