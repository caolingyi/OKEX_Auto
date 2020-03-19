using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKEX.Auto.Core.Domain.Interface.OKEX;
using OKEX.Auto.Core.Domain.Models.Okex.Futures;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace OKEX.Auto.Core.Domain.Manager.OKEX   
{
    /// <summary>
    /// okex 交割合约
    /// </summary>
    public class OkexFutureManager : OkexSdkApi,IOkexFutureManager
    {
        private string FUTURES_SEGMENT = "api/futures/v3";
        public OkexFutureManager(string apiKey, string secret, string passPhrase) : base(apiKey, secret, passPhrase) { }
        public OkexFutureManager(Guid userID) : base(userID) { }
        public OkexFutureManager() : base() { }

        #region 公共接口

        /// <summary>
        /// 获取K线数据
        /// </summary>
        /// <param name="instrument_id">合约ID，如BTC-USD-180213</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="granularity">时间粒度，以秒为单位，必须为60的倍数</param>
        /// <returns></returns>
        public async Task<List<OkexFutureCandle>> GetCandlesDataAsync(string instrument_id, DateTime? start, DateTime? end, int? granularity = 60)
        {
            //https://www.okex.me/api/futures/v3/instruments/BTC-USD-191227/candles
            var url = $"{this.BASEURL}{this.FUTURES_SEGMENT}/instruments/{instrument_id}/candles";
            using (var client = new HttpClient(new OkexHttpInterceptor(this._apiKey, this._secret, this._passPhrase, null)))
            {
                var queryParams = new Dictionary<string, string>();
                if (start.HasValue)
                {
                    queryParams.Add("start", TimeZoneInfo.ConvertTimeToUtc(start.Value).ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                }
                if (end.HasValue)
                {
                    queryParams.Add("end", TimeZoneInfo.ConvertTimeToUtc(end.Value).ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                }
                if (granularity.HasValue)
                {
                    queryParams.Add("granularity", (granularity??60).ToString());
                }
                var encodedContent = new FormUrlEncodedContent(queryParams);
                var paramsStr = await encodedContent.ReadAsStringAsync();
                var res = await client.GetAsync($"{url}?{paramsStr}");
                var response = await res.Content.ReadAsStringAsync();

                List<List<string>> temp = JsonConvert.DeserializeObject<List<List<string>>>(response);
                List<OkexFutureCandle> result = temp.Select(p => new OkexFutureCandle()
                {
                    timestamp = p[0],
                    open = p[1],
                    high = p[2],
                    low = p[3],
                    close = p[4],
                    volume = p[5],
                    currency_volume = p[6],

                }).ToList();
                return result;
            }
        }


        /// <summary>
        /// 获取某个ticker信息
        /// </summary>
        /// <param name="instrument_id">合约ID，如BTC-USD-180213</param>
        /// <returns></returns>
        public async Task<OkexFutureTicker> GetTickerByInstrumentIdAsync(string instrument_id)
        {
            var url = $"{this.BASEURL}{this.FUTURES_SEGMENT}/instruments/{instrument_id}/ticker";
            using (var client = new HttpClient(new OkexHttpInterceptor(this._apiKey, this._secret, this._passPhrase, null)))
            {
                var res = await client.GetAsync(url);
                var response = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OkexFutureTicker>(response);
            }
        }

        #endregion

    }
}
