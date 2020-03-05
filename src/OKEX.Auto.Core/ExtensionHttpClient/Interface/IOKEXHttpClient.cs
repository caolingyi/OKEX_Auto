using OKEX.Auto.Core.ExtensionHttpClient.Base;
using System.IO;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.ExtensionHttpClient.Interface
{
    public interface IOKEXHttpClient : IHttpClient
    {
        Task<Stream> GetStreamAsync(string uri, string authorizationToken = null,
            string authorizationMethod = "Bearer");
    }
}
