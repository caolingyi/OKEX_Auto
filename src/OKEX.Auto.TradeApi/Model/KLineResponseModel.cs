using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKEX.Auto.TradeApi.Model
{
    public class KLineResponseModel
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public String timestamp { get; set; }

        /// <summary>
        /// 开盘价格
        /// </summary>
        public String open { get; set; }
        /// <summary>
        /// 最高价格
        /// </summary>
        public String high { get; set; }
        /// <summary>
        /// 最低价格
        /// </summary>
        public String low { get; set; }
        /// <summary>
        /// 收盘价格
        /// </summary>
        public String close { get; set; }
        /// <summary>
        /// 交易量（张）
        /// </summary>
        public String volume { get; set; }
        /// <summary>
        /// 按币种折算的交易量
        /// </summary>
        public String currency_volume { get; set; }
    }
}
