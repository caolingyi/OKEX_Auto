using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKEX.Auto.TradeApi.Model
{
    public class KLineRequestModel
    {
        /// <summary>
        /// 合约ID，如BTC-USD-180213,BTC-USDT-191227
        /// </summary>
        public String instrument_id { get; set; }

        /// <summary>
        /// 开始时间（ISO 8601标准，例如：2018-06-20T02:31:00Z）
        /// </summary>
        public String start { get; set; }

        /// <summary>
        /// 结束时间（ISO 8601标准，例如：2018-06-20T02:31:00Z）
        /// </summary>
        public String end { get; set; }

        /// <summary>
        /// 时间粒度，以秒为单位，默认值60。如[60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800]，详见下解释说明
        /// </summary>
        public String granularity { get; set; }
    }
}
