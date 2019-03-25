namespace DangEasy.Caching.MemoryCache
{
    public class CacheKey
    {
        public static string Build<TCallingClass, TReturnedType>(string value)
        {
            var cacheKey = $"{typeof(TCallingClass)}_{typeof(TReturnedType)}_{value}";

            return cacheKey;
        }
    }
}
