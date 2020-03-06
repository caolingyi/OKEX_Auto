using OKEX.Auto.Core.ExtensionHttpClient.Base;
using OKEX.Auto.Core.ExtensionHttpClient.Interface;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.ExtensionHttpClient.Clients
{
    public class OKEXHttpClient : StandardHttpClient, IOKEXHttpClient
    {
        private readonly System.Net.Http.HttpClient _client;

        public OKEXHttpClient(System.Net.Http.HttpClient httpClient, OKEXSettings fileSystemConfig) : base(httpClient)
        {
            httpClient.BaseAddress = new Uri(fileSystemConfig.BaseUrl);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            _client = httpClient;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            if (!string.IsNullOrEmpty(authorizationToken))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            var response = await _client.SendAsync(requestMessage);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
