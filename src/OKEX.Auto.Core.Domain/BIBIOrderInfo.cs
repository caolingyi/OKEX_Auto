using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain
{
    public class BIBIOrderInfo
    {
        /// <summary>
        ///  订单ID
        /// </summary>
        public String order_id { get; set; }
        /// <summary>
        ///  用户设置的订单ID
        /// </summary>
        public String client_oid { get; set; }
        /// <summary>
        ///  委托价格
        /// </summary>
        public String price { get; set; }
        /// <summary>
        ///  委托数量（交易货币数量）
        /// </summary>
        public String size { get; set; }
        /// <summary>
        ///  买入金额，市价买入时返回
        /// </summary>
        public String notional { get; set; }
        /// <summary>
        ///  币对名称
        /// </summary>
        public String instrument_id { get; set; }
        /// <summary>
        ///  limit或market（默认是limit）
        /// </summary>
        public String type { get; set; }
        /// <summary>
        ///  buy 或 sell
        /// </summary>
        public String side { get; set; }
        /// <summary>
        ///  订单创建时间
        /// </summary>
        public String timestamp { get; set; }
        /// <summary>
        ///  已成交数量
        /// </summary>
        public String filled_size { get; set; }
        /// <summary>
        ///  已成交金额
        /// </summary>
        public String filled_notional { get; set; }
        /// <summary>
        ///  0：普通委托 1：只做Maker（Post only） 2：全部成交或立即取消（FOK） 3：立即成交并取消剩余（IOC）
        /// </summary>
        public String order_type { get; set; }
        /// <summary>
        ///  订单状态 -2：失败 -1：撤单成功 0：等待成交 1：部分成交 2：完全成交 3：下单中 4：撤单中
        /// </summary>
        public String state { get; set; }
        /// <summary>
        ///  成交均价
        /// </summary>
        public String price_avg { get; set; }
    }
}
