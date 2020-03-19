using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.Models.Okex.Futures
{
    /// <summary>
    /// okex 交割合约 实时交易行情
    /// </summary>
    public class OkexFutureTicker
    {
        /// <summary>
        /// 合约类型 id ，如BTC-USD-180213,BTC-USDT-191227
        /// </summary>
        public string instrument_id { get; set; }

        /// <summary>
        /// 最新成交价
        /// </summary>
        public decimal last { get; set; }
        /// <summary>
        /// 最新成交的数量
        /// </summary>
        public string last_qty { get; set; }


        /// <summary>
        /// 买一价
        /// </summary>
        public string best_bid { get; set; }
        /// <summary>
        /// 买一价对应的数量
        /// </summary>
        public string best_bid_size { get; set; }

        /// <summary>
        /// 卖一价
        /// </summary>
        public string best_ask { get; set; }
        /// <summary>
        /// 卖一价对应的量
        /// </summary>
        public string best_ask_size { get; set; }

        /// <summary>
        /// 24小时最高价
        /// </summary>
        public string high_24h { get; set; }
        /// <summary>
        /// 24小时最低价
        /// </summary>
        public string low_24h { get; set; }
        /// <summary>
        /// 24小时成交量，按张数统计
        /// </summary>
        public string volume_24h { get; set; }
        /// <summary>
        /// 系统时间戳
        /// </summary>
        public string timestamp { get; set; }
     
       
       
    }
}
