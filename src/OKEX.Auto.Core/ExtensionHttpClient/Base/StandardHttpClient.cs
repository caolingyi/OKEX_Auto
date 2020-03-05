using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.ExtensionHttpClient.Base
{
    /// <summary>
    /// httpclient
    /// </summary>
    public class StandardHttpClient : IHttpClient
    {
        private readonly HttpClient _client;

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="httpClient"></param>
        public StandardHttpClient(HttpClient httpClient)
        {
            _client = httpClient;
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            if (!string.IsNullOrEmpty(authorizationToken))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            var response = await _client.SendAsync(requestMessage);
            ThrowInternalServerError(response);
            return response;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            if (!string.IsNullOrEmpty(authorizationToken))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            var response = await _client.SendAsync(requestMessage);
            ThrowInternalServerError(response);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="mediaType"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer", string mediaType = "application/json")
        {
            return await DoPostPutAsync<T>(HttpMethod.Post, uri, item, mediaType, authorizationToken, authorizationMethod);
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync<T>(HttpMethod.Put, uri, item, null, authorizationToken, authorizationMethod);
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="message"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            if (!string.IsNullOrEmpty(authorizationToken))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            var response = await _client.SendAsync(message);
            ThrowInternalServerError(response);
            return response;
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string mediaType = "application/json", string authorizationToken = null,
            string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }
            var requestMessage = new HttpRequestMessage(method, uri);
            if (!string.IsNullOrEmpty(authorizationToken))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            if (mediaType == "application/json")
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, mediaType);
            }
            else if (mediaType == "application/x-www-form-urlencoded" || mediaType == "text/xml")
            {
                requestMessage.Content = new StringContent(item.ToString(), Encoding.UTF8, mediaType);
            }
            var response = await _client.SendAsync(requestMessage);
            ThrowInternalServerError(response);
            return response;
        }

        void ThrowInternalServerError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
        }
    }
}
