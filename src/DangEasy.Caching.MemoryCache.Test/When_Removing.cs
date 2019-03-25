using System.Collections.Generic;
using Xunit;

namespace DangEasy.Caching.MemoryCache.Test
{
    [Collection("Non-Parallel")]
    public class When_Removing
    {
        Cache _cache;
        public When_Removing()
        {
            _cache = Cache.Instance;
        }


        [Fact]
        public void Item_Is_Removed()
        {
            var cacheKey = CacheKey.Build<When_Removing, string>("Item_Is_Removed");

            _cache.Add(cacheKey, "hello");
            _cache.Remove(cacheKey);

            var result = _cache.Get<string>(cacheKey);

            Assert.Null(result);
        }


        [Fact]
        public void NonExisting_Item_Is_Doesnt_Crash()
        {
            _cache.Remove("not_here");
        }


        [Fact]
        public void NonExisting_Prefix_Doesnt_Crash()
        {
            _cache.RemoveByPrefix("something");
        }


        [Fact]
        public void Items_Are_Removed_By_Prefix()
        {
            var cacheKey = CacheKey.Build<When_Removing, string>("Items_Are_Removed_By_Prefix");

            _cache.Add(cacheKey, "hello");
            _cache.RemoveByPrefix(typeof(When_Removing).ToString());

            var result = _cache.Get<string>(cacheKey);

            Assert.Null(result);
        }
    }
}
