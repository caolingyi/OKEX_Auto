using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OKEX.Auto.Core.Domain.Interface.OKEX;
using OKEX.Auto.Core.Domain.Manager.OKEX;
using OKEX.Auto.Core.Domain.Models.Okex.Futures;

namespace OKEX.Auto.TradeApi.Controllers.Okex
{
    [Route("api/OkexFuture")]
    [ApiController]
    public class OkexFutureController : ControllerBase
    {
        //public IOkexFutureManager _OkexFutureManager;
        private readonly ILogger _logger;
        private readonly string defaultInstrumentID = "BTC-USDT-200626";
        //private readonly ILogger<OkexFutureController> _logger;

        public OkexFutureController(
            ILogger<OkexFutureController> logger
            //, IOkexFutureManager IOkexFutureManager
            )
        {
            _logger = logger;
            //_OkexFutureManager = IOkexFutureManager;
        }


        /// <summary>
        /// 获取K线数据
        /// </summary>
        /// <param name="instrument_id">合约ID，如BTC-USD-180213</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="granularity">时间粒度，以秒为单位，必须为60的倍数</param>
        /// <returns></returns>
        [Route("GetCandlesData")]
        [HttpGet]
        public async Task<List<OkexFutureCandle>> GetCandlesDataAsync(string instrument_id, DateTime? start, DateTime? end, int? granularity)
        {
            instrument_id = string.IsNullOrWhiteSpace(instrument_id) ? defaultInstrumentID : instrument_id;
            start = start ?? DateTime.Now.AddDays(-1);
            end = end ?? DateTime.Now;
            var data = await new OkexFutureManager().GetCandlesDataAsync(instrument_id, start, end, granularity);
            return data;
        }


        [Route("GetTickerByInstrumentId")]
        [HttpGet]
        public async Task<OkexFutureTicker> GetTickerByInstrumentIdAsync(string instrument_id)//BTC-USDT-200626
        {
            instrument_id = string.IsNullOrWhiteSpace(instrument_id) ? defaultInstrumentID : instrument_id;
            var data = await new OkexFutureManager().GetTickerByInstrumentIdAsync(instrument_id);
            return data;
        }




    }
}