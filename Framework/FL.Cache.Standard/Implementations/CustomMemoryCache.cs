using FL.Cache.Standard.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace FL.Cache.Standard.Implementations
{
    public class CustomMemoryCache<T> : ICustomMemoryCache<T>
    {
        private readonly MemoryCache cache;

        public CustomMemoryCache()
        {
             this.cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void Add(object key, T items)
        {
            //var entryOptions = new MemoryCacheEntryOptions().;
            this.cache.Set(key, items, TimeSpan.FromMinutes(5));
        }

        public T Get(object key)
        {
            var cacheList = this.cache.Get<T>(key);
            
            return cacheList;
        }

        public T GetOrCreate(object key, Func<T> createItem)
        {
            T cacheEntry;
            if (!cache.TryGetValue(key, out cacheEntry))// Look for cache key.
            {
                // Key not in cache, so get data.
                cacheEntry = createItem();

                // Save data in cache.
                cache.Set(key, cacheEntry);
            }

            return cacheEntry;
        }
    }
}
