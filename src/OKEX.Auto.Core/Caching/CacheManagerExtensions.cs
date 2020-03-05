using System;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.Caching
{
    /// <summary>
    /// 缓存扩展
    /// </summary>
    public static class CacheManagerExtensions
    {
        private static int DefaultCacheTimeMinutes = 60;
        public static async Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, Func<Task<T>> acquire)
        {
            return await GetAsync(cacheManager, key, DefaultCacheTimeMinutes, acquire);
        }
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, DefaultCacheTimeMinutes, acquire);
        }

        public static async Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<Task<T>> acquire)
        {
            if (await cacheManager.IsSetAsync(key))
                return await cacheManager.GetAsync<T>(key);

            var result = await acquire();

            if (cacheTime > 0)
                await cacheManager.SetAsync(key, result, cacheTime);

            return result;
        }
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet(key))
                return cacheManager.Get<T>(key);

            var result = acquire();

            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);

            return result;
        }
    }
}
