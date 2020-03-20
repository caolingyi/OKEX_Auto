using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.Models.Okex.Futures
{
    /// <summary>
    ///  交割合约 撤单 返回数据
    /// </summary>
    public class OkexFurtueCancelOrderReponse : OkexFurtueMakeOrderReponse
    {
        /// <summary>
        /// 合约ID，如BTC-USD-180213,,BTC-USDT-191227
        /// </summary>
        public string instrument_id { get; set; }

    }
}
