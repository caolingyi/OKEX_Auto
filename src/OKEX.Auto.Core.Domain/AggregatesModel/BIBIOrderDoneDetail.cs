using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel
{
    public class BIBIOrderDoneDetail
    {
        /// <summary>
        ///  账单ID
        /// </summary>
        public String ledger_id { get; set; }
        /// <summary>
        ///  成交ID
        /// </summary>
        public String trade_id { get; set; }
        /// <summary>
        ///  币对名称
        /// </summary>
        public String instrument_id { get; set; }
        /// <summary>
        ///  成交价格
        /// </summary>
        public String price { get; set; }
        /// <summary>
        ///  成交数量
        /// </summary>
        public String size { get; set; }
        /// <summary>
        ///  订单ID
        /// </summary>
        public String order_id { get; set; }
        /// <summary>
        ///  订单成交时间
        /// </summary>
        public String timestamp { get; set; }
        /// <summary>
        ///  流动性方向（T 或 M）
        /// </summary>
        public String exec_type { get; set; }
        /// <summary>
        ///  手续费
        /// </summary>
        public String fee { get; set; }
        /// <summary>
        ///  账单方向（buy、sell）
        /// </summary>
        public String side { get; set; }
        /// <summary>
        ///  币种
        /// </summary>
        public String currency { get; set; }
    }
}
