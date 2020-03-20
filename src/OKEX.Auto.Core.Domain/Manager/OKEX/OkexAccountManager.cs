using Newtonsoft.Json;
using OKEX.Auto.Core.Domain.Interface.OKEX;
using OKEX.Auto.Core.Domain.Models.Okex.Futures;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using OKEX.Auto.Core.Domain.Models.Okex.Account;

namespace OKEX.Auto.Core.Domain.Manager.OKEX
{
    public class OkexAccountManager : OkexSdkApi, IOkexAccountManager
    {
        private string ACCOUNT_SEGMENT = "api/account/v3";
        public OkexAccountManager(string apiKey, string secret, string passPhrase) : base(apiKey, secret, passPhrase) { }
        public OkexAccountManager(Guid userID) : base(userID) { }
        public OkexAccountManager() : base() { }

        /// <summary>
        /// 获取账户钱包信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<OkexAccountWallet>> getWalletInfoAsync()
        {
            var url = $"{this.BASEURL}{this.ACCOUNT_SEGMENT}/wallet";
            using (var client = new HttpClient(new OkexHttpInterceptor(this._apiKey, this._secret, this._passPhrase, null)))
            {
                var res = await client.GetAsync(url);
                var response = await res.Content.ReadAsStringAsync();
                List<List<string>> temp = JsonConvert.DeserializeObject<List<List<string>>>(response);
                List<OkexAccountWallet> result = temp.Select(p => new OkexAccountWallet()
                {
                    currency = p[0],
                    balance = Convert.ToDecimal(p[1]),
                    hold = Convert.ToDecimal(p[2]),
                    available = Convert.ToDecimal(p[3]),
                }).ToList();
                return result;
            }
        }

        public async Task<List<OkexAccountWithDrawalFee>> getWithDrawalFeeAsync(string currency)
        {
            var url = $"{this.BASEURL}{this.ACCOUNT_SEGMENT}/withdrawal/fee";
            using (var client = new HttpClient(new OkexHttpInterceptor(this._apiKey, this._secret, this._passPhrase, null)))
            {
                var queryParams = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(currency))
                {
                    queryParams.Add("currency", currency);
                }
                var encodedContent = new FormUrlEncodedContent(queryParams);
                var paramsStr = await encodedContent.ReadAsStringAsync();
                url = string.IsNullOrWhiteSpace(paramsStr) ?url: $"{url}?{paramsStr}";
                 var res = await client.GetAsync(url);
                var response = await res.Content.ReadAsStringAsync();
                List<OkexAccountWithDrawalFee> result = JsonConvert.DeserializeObject<List<OkexAccountWithDrawalFee>>(response);
                return result;
            }
        }

    }
}
