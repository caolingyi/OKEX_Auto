using Newtonsoft.Json;
using OKEX.Auto.Core.Domain.Interface.OKEX;
using OKEX.Auto.Core.Domain.Models.Okex.Futures;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

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


        /// <summary>
        /// 通过订单ID获取单个订单信息
        /// </summary>
        /// <param name="instrument_id">合约ID，如BTC-USD-180213</param>
        /// <param name="order_id">订单ID</param>
        /// <returns></returns>
        public async Task<string> GetOrderByIdAsync(string instrument_id, string order_id)
        {
            var url = $"{this.BASEURL}{this.FUTURES_SEGMENT}/orders/{instrument_id}/{order_id}";
            using (var client = new HttpClient(new OkexHttpInterceptor(this._apiKey, this._secret, this._passPhrase, null)))
            {
                var res = await client.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                return contentStr;
            }
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="instrument_id">合约ID，如BTC-USD-180213</param>
        /// <param name="type">1:开多2:开空3:平多4:平空</param>
        /// <param name="price">每张合约的价格</param>
        /// <param name="size">买入或卖出合约的数量（以张计数）</param>
        /// <param name="leverage">要设定的杠杆倍数，10或20</param>
        /// <param name="client_oid">由您设置的订单ID来识别您的订单</param>
        /// <param name="match_price">是否以对手价下单(0:不是 1:是)，默认为0，当取值为1时。price字段无效</param>
        /// <returns></returns>
        public async Task<OkexFurtueMakeOrderReponse> MakeOrderAsync(OkexFurtueMakeOrderRequest request)
        {
            var url = $"{this.BASEURL}{this.FUTURES_SEGMENT}/order";
            var bodyStr = JsonConvert.SerializeObject(request);
            using (var client = new HttpClient(new OkexHttpInterceptor(this._apiKey, this._secret, this._passPhrase, bodyStr)))
            {
                var res = await client.PostAsync(url, new StringContent(bodyStr, Encoding.UTF8, "application/json"));
                var response = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OkexFurtueMakeOrderReponse>(response);
            }
        }



        /// <summary>
        /// 撤销指定订单
        /// </summary>
        /// <param name="instrument_id">合约ID，如BTC-USD-180213</param>
        /// <param name="order_id">服务器分配订单ID</param>
        /// <returns></returns>
        public async Task<OkexFurtueCancelOrderReponse> CancelOrderAsync(string instrument_id, string order_id)
        {
            var url = $"{this.BASEURL}{this.FUTURES_SEGMENT}/cancel_order/{instrument_id}/{order_id}";
            using (var client = new HttpClient(new OkexHttpInterceptor(this._apiKey, this._secret, this._passPhrase, "")))
            {
                var res = await client.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
                var response = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OkexFurtueCancelOrderReponse>(response);
            }
        }

        /// <summary>
        /// 获取成交数据
        /// </summary>
        /// <param name="instrument_id">合约ID，如BTC-USD-180213</param>
        /// <param name="from">分页游标开始</param>
        /// <param name="to">分页游标截至</param>
        /// <param name="limit">分页数据数量，默认100</param>
        /// <returns></returns>
        //public async Task<JContainer> GetTradesAsync(string instrument_id, int? after, int? before, int? limit)
        //{
        //    var url = $"{this.BASEURL}{this.FUTURES_SEGMENT}/instruments/{instrument_id}/trades";
        //    using (var client = new HttpClient(new OkexHttpInterceptor(this._apiKey, this._secret, this._passPhrase, null)))
        //    {
        //        var queryParams = new Dictionary<string, string>();
        //        if (after.HasValue)
        //        {
        //            queryParams.Add("after", after.Value.ToString());
        //        }
        //        if (before.HasValue)
        //        {
        //            queryParams.Add("before", before.Value.ToString());
        //        }
        //        if (limit.HasValue)
        //        {
        //            queryParams.Add("limit", limit.Value.ToString());
        //        }
        //        var encodedContent = new FormUrlEncodedContent(queryParams);
        //        var paramsStr = await encodedContent.ReadAsStringAsync();
        //        var res = await client.GetAsync($"{url}?{paramsStr}");
        //        var contentStr = await res.Content.ReadAsStringAsync();
        //        if (contentStr[0] == '[')
        //        {
        //            return JArray.Parse(contentStr);
        //        }
        //        return JObject.Parse(contentStr);
        //    }
        //}

    }
}
