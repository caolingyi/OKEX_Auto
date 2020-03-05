using System.Net.Http;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.ExtensionHttpClient.Base
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer");
        Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer");
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer", string mediaType = "application/json");
        Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer");
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, string authorizationToken = null, string authorizationMethod = "Bearer");
    }
}
