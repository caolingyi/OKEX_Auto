using Newtonsoft.Json.Linq;
using OKEX.Auto.Core.Domain.Models.Okex.Futures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.Domain.Interface.OKEX
{
    /// <summary>
    ///  okex 交割合约 相关接口
    /// </summary>
    public interface IOkexFutureManager
    {
        Task<List<OkexFutureCandle>> GetCandlesDataAsync(string instrument_id, DateTime? start, DateTime? end, int? granularity);

        Task<OkexFutureTicker> GetTickerByInstrumentIdAsync(string instrument_id);

        Task<OkexFurtueMakeOrderReponse> MakeOrderAsync(OkexFurtueMakeOrderRequest request);

        Task<OkexFurtueCancelOrderReponse> CancelOrderAsync(string instrument_id, string order_id);



    }
}
