using System.Collections.Generic;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.Caching
{
    public interface ICacheManager
    {
        bool IsSet(string key);
        Task<bool> IsSetAsync(string key);
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);
        void Remove(string key);
        Task RemoveAsync(string key);
        void Set(string key, object data, int cacheTime);
        Task SetAsync(string key, object data, int cacheTime);
        void RemoveByPattern(string pattern);
        Task RemoveByPatternAsync(string pattern);
        Task ClearAsync();
        Task<List<T>> GetByPatternAsync<T>(string pattern);
    }
}
