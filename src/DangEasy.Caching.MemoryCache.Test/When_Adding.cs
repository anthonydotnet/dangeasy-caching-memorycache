using System.Threading;
using Xunit;

namespace DangEasy.Caching.MemoryCache.Test
{
    public class When_Adding
    {
        Cache _cache;
        public When_Adding()
        {
            _cache = new Cache();
        }


        [Fact]
        public void Item_Is_Added_For_Expected_Time()
        {
            var cacheKey = CacheKey.Build<When_Adding, string>("Value_Times_Out");

            _cache.Add(cacheKey, "hello", 1);
            Thread.Sleep(1100); // this is a bit hacky but i can't think of a more elegant way to test

            var result = _cache.Get<string>(cacheKey);

            Assert.Null(result);
        }


        [Fact]
        public void Existing_Item_Causes_Error()
        {
            var cacheKey = CacheKey.Build<When_Adding, string>("Existing_Item_Causes_Error");

            _cache.Add(cacheKey, "hello");

            var result = _cache.Add(cacheKey, "goodbye");
            Assert.False(result);
        }
    }
}
