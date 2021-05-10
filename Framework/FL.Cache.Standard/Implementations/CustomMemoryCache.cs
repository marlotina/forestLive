using FL.Cache.Standard.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace FL.Cache.Standard.Implementations
{
    public class CustomMemoryCache<T> : ICustomMemoryCache<T>
    {
        private readonly MemoryCache cache;
        private readonly ILogger<CustomMemoryCache<T>> iLogger; 
        public CustomMemoryCache(ILogger<CustomMemoryCache<T>> iLogger)
        {
            this.iLogger = iLogger;
            this.cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void Add(object key, T items)
        {
            try
            {
                //var entryOptions = new MemoryCacheEntryOptions().;
                this.cache.Set(key, items, TimeSpan.FromMinutes(5));
            }
            catch (Exception ex) 
            {
                this.iLogger.LogError(ex.Message);
            }
        }

        public T Get(object key)
        {
            T cacheList = default(T);
            try
            {
                cacheList = this.cache.Get<T>(key);
                return cacheList;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return cacheList;
        }
        
        public T GetOrCreate(object key, Func<T> createItem)
        {
            T cacheEntry = default(T);
            try
            {
                if (!cache.TryGetValue(key, out cacheEntry))// Look for cache key.
                {
                    // Key not in cache, so get data.
                    cacheEntry = createItem();

                    // Save data in cache.
                    cache.Set(key, cacheEntry);
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
            }

            return cacheEntry;
        }
    }
}
