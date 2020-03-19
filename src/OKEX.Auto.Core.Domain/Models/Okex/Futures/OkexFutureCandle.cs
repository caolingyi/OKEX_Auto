using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.Models.Okex.Futures
{
    /// <summary>
    /// / okex 交割合约 K线【蜡烛】
    /// </summary>
    public class OkexFutureCandle
    {
        /// <summary>
        /// 	开始时间
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 	开盘价格
        /// </summary>
        public string open { get; set; }
        /// <summary>
        /// 最高价格
        /// </summary>
        public string high { get; set; }
        /// <summary>
        /// 	最低价格
        /// </summary>
        public string low { get; set; }
        /// <summary>
        /// 	收盘价格
        /// </summary>
        public string close { get; set; }
        /// <summary>
        /// 交易量（张）
        /// </summary>
        public string volume { get; set; }
        /// <summary>
        /// 按币种折算的交易量
        /// </summary>
        public string currency_volume { get; set; }


    }
}
