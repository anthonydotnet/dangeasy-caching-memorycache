using System;
using System.Linq;
using DangEasy.Interfaces.Caching;

namespace DangEasy.Caching.MemoryCache
{
    public class Cache : ICache
    {
        public static Cache Instance { get; } = new Cache();


        public bool Add(string key, object value, int? cacheSeconds = null)
        {
            var memoryCache = System.Runtime.Caching.MemoryCache.Default;

            var cacheDuration = cacheSeconds != null ? DateTime.Now.AddSeconds(cacheSeconds.Value) : DateTime.MaxValue;

            return memoryCache.Add(key, value, cacheDuration);
        }


        public T Get<T>(string key)
        {
            var memoryCache = System.Runtime.Caching.MemoryCache.Default;
            return (T)memoryCache.Get(key);
        }


        public T Get<T>(string cacheKey, Func<T> hydrationFunction, int? cacheSeconds = null)
            where T : class
        {
            if (Get<T>(cacheKey) is T cachedItem)
            {
                return cachedItem;
            }

            var itemToBeCached = hydrationFunction();

            if (itemToBeCached == null)
            {
                return null;
            }

            Add(cacheKey, itemToBeCached, cacheSeconds);

            return itemToBeCached;
        }


        public bool Remove(string key)
        {
            var memoryCache = System.Runtime.Caching.MemoryCache.Default;

            if (memoryCache.Any(x => x.Key.Equals(key)))
            {
                return memoryCache.Remove(key) != null;
            }

            return false;
        }


        public void RemoveByPrefix(string cachePrefix)
        {
            var memoryCache = System.Runtime.Caching.MemoryCache.Default;

            var entries = memoryCache.Where(x => x.Key.StartsWith(cachePrefix, StringComparison.Ordinal));
            foreach (var entry in entries)
            {
                memoryCache.Remove(entry.Key);
            }
            //TODO: what should we do/return if some entries failed?
        }
    }
}