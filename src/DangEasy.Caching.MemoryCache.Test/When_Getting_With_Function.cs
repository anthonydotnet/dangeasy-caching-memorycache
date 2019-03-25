using Xunit;

namespace DangEasy.Caching.MemoryCache.Test
{
    public class When_Getting_With_Function
    {
        Cache _cache;
        public When_Getting_With_Function()
        {
            _cache = new Cache();
        }




        [Fact]
        public void Value_Is_Returned()
        {
            var cacheKey = CacheKey.Build<When_Getting_With_Function, string>("Value_Is_Returned");

            var result = _cache.Get<string>(cacheKey, MyValueMethod);

            Assert.Equal("Hello", result);
        }


        [Fact]
        public void NonExisting_Item_Is_Handled()
        {
            var cacheKey = CacheKey.Build<When_Getting_With_Function, string>("NonExisting_Item_Is_Handled");

            var result = _cache.Get<string>(cacheKey, MyNullValueMethod);

            Assert.Null(result);
        }


        [Fact]
        public void NonExisting_Custom_Class_Is_Handled()
        {
            var cacheKey = CacheKey.Build<When_Getting_With_Function, string>("NonExisting_Custom_Class_Is_Handled");

            var result = _cache.Get<MyClass>(cacheKey);

            Assert.Null(result);
        }


        // helpers
        string MyValueMethod()
        {
            return "Hello";
        }


        string MyNullValueMethod()
        {
            return null;
        }

        class MyClass
        {
        }
    }
}
