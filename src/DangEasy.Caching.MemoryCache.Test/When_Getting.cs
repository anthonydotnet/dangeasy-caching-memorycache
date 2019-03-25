using System;
using Xunit;

namespace DangEasy.Caching.MemoryCache.Test
{
    public class When_Getting
    {
        Cache _cache;
        public When_Getting()
        {
            _cache = Cache.Instance;
        }


        [Fact]
        public void Value_Is_Returned()
        {
            var cacheKey = CacheKey.Build<When_Getting, string>("Value_Is_Returned");

            _cache.Add(cacheKey, "hello", new TimeSpan(0, 0, 1));

            var result = _cache.Get<string>(cacheKey);

            Assert.Equal("hello", result);
        }


        [Fact]
        public void NonExisting_Item_Is_Handled()
        {
            var cacheKey = CacheKey.Build<When_Getting, string>("NonExisting_Item_Is_Handled");

            var result = _cache.Get<string>(cacheKey);

            Assert.Null(result);
        }

        [Fact]
        public void NonExisting_Custom_Class_Is_Handled()
        {
            var cacheKey = CacheKey.Build<When_Getting, string>("NonExisting_Custom_Class_Is_Handled");

            var result = _cache.Get<MyClass>(cacheKey);

            Assert.Null(result);
        }


        [Fact]
        public void NonExisting_Date_Is_Handled()
        {
            var cacheKey = CacheKey.Build<When_Getting, string>("NonExisting_Date_Is_Handled");

            var result = _cache.Get<DateTime?>(cacheKey);

            Assert.Null(result);
        }


        [Fact]
        public void NonExisting_Of_Interface_Is_Handled()
        {
            var cacheKey = CacheKey.Build<When_Getting, string>("NonExisting_Of_Interface_Is_Handled");

            var result = _cache.Get<IMyInterface>(cacheKey);

            Assert.Null(result);
        }
    }


    // helpers
    interface IMyInterface { }

    class MyClass { }
}
