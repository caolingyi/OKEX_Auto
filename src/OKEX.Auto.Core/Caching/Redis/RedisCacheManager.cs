using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OKEX.Auto.Core.Caching.Redis
{
    public class RedisCacheManager : ICacheManager 
    {
        private readonly RedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;
        public RedisCacheManager(RedisConnectionWrapper redisConnectionWrapper)
        {
            _connectionWrapper = redisConnectionWrapper;
            _db = _connectionWrapper.GetDatabase();
        }
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Key of cached item</param>
        /// <returns>The cached value associated with the specified key</returns>
        public virtual T Get<T>(string key)
        {
            var serializedItem = _db.StringGet(key);
            if (!serializedItem.HasValue)
                return default(T);

            var item = JsonConvert.DeserializeObject<T>(serializedItem);
            if (item == null)
                return default(T);
            return item;
        }
        public virtual List<T> HashGetAll<T>(string key)
        {
            var serializedItem = _db.HashGetAll(key);
            if (!serializedItem.Any())
                return default(List<T>);
            var list = new List<T>();
            foreach (var serializedEntry in serializedItem)
            {
                var item = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(serializedEntry));
                list.Add(item);
            }
            if (!list.Any())
                return default(List<T>);
            return list;
        }
        public virtual List<T> HashGetAllValue<T>(string key)
        {
            var serializedItem = _db.HashGetAll(key);
            if (!serializedItem.Any())
                return default(List<T>);
            var list = new List<T>();
            foreach (var serializedEntry in serializedItem)
            {
                var item = JsonConvert.DeserializeObject<T>(serializedEntry.Value);
                list.Add(item);
            }
            if (!list.Any())
                return default(List<T>);
            return list;
        }

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;
            //set cache time
            var expiresIn = TimeSpan.FromMinutes(cacheTime);
            //serialize item
            var serializedItem = SerializeObject(data);

            //and set it to cache
            _db.StringSet(key, serializedItem, expiresIn);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        public virtual bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }
        public virtual async Task<bool> IsSetAsync(string key)
        {
            return await _db.KeyExistsAsync(key);
        }
        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        public virtual void Remove(string key)
        {
            _db.KeyDelete(key);
        }
        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        public virtual async Task RemoveAsync(string key)
        {
            //remove item from caches
            await _db.KeyDeleteAsync(key);
        }
        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual async void Clear()
        {
            await this.ClearAsync();
        }
        public virtual async void RemoveByPattern(string pattern)
        {
            await this.RemoveByPatternAsync(pattern);
        }
        public virtual async Task RemoveByPatternAsync(string pattern)
        {
            foreach (var endPoint in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(endPoint);
                var keys = server.Keys(database: _db.Database, pattern: $"*{pattern}*");
                await _db.KeyDeleteAsync(keys.ToArray());
            }
        }
        public virtual async Task<T> GetAsync<T>(string key)
        {
            var serializedItem = await _db.StringGetAsync(key);
            if (!serializedItem.HasValue)
                return default(T);

            var item = JsonConvert.DeserializeObject<T>(serializedItem);
            if (item == null)
                return default(T);

            return item;
        }
        public virtual async Task<List<T>> GetByPatternAsync<T>(string pattern)
        {
            foreach (var endPoint in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(endPoint);
                var keys = server.Keys(database: _db.Database, pattern: $"*{pattern}*");
                var serializedItem = await _db.StringGetAsync(keys.ToArray());
                if (!serializedItem.Any())
                    return default(List<T>);
                return serializedItem.Select(s => JsonConvert.DeserializeObject<T>(s)).ToList();
            }
            return default(List<T>);
        }
        public virtual async Task SetAsync(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            //set cache time
            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            //serialize item
            var serializedItem = SerializeObject(data);

            //and set it to cache
            await _db.StringSetAsync(key, serializedItem, expiresIn);
        }
        public virtual async Task ClearAsync()
        {
            foreach (var endPoint in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(endPoint);

                //that's why we manually delete all elements
                var keys = server.Keys(database: _db.Database);

                await _db.KeyDeleteAsync(keys.ToArray());
            }
        }
        private static JsonSerializerSettings GetJsonSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return settings;
        }
        static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, GetJsonSettings());
        }
    }
}
