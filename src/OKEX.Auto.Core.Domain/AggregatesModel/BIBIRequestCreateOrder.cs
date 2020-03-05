using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel
{
    public class BIBIRequestCreateOrder
    {
        /// <summary>
        ///  由您设置的订单ID来识别您的订单,格式是字母（区分大小写）+数字 或者 纯字母（区分大小写），1-32位字符 （不能重复）
        /// </summary>
        public String client_oid { get; set; }

        /// <summary>
        /// limit或market（默认是limit）。当以market（市价）下单，order_type只能选择0（普通委托）
        /// </summary>
        public String type { get; set; }
        /// <summary>
        /// buy 或 sell
        /// </summary>
        public String side { get; set; }
        /// <summary>
        /// 	币对名称
        /// </summary>
        public String instrument_id { get; set; }
        /// <summary>
        /// 参数填数字0：普通委托（order type不填或填0都是普通委托） 1：只做Maker（Post only） 2：全部成交或立即取消（FOK） 3：立即成交并取消剩余（IOC）
        /// </summary>
        public String order_type { get; set; }
        /// <summary>
        /// 限价单特殊参数 	价格
        /// </summary>
        public String priceLimit { get; set; }
        /// <summary>
        /// 限价单特殊参数 	买入或卖出的数量;
        /// </summary>
        public String sizeLimit { get; set; }
        /// <summary>
        /// 市价单特殊参数 卖出数量，市价卖出时必填size
        /// </summary>
        public String sizeMarket { get; set; }

        /// <summary>
        /// 市价单特殊参数 买入金额，市价买入时必填notional
        /// </summary>
        public String notional { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public String order_id { get; set; }

        /// <summary>
        /// 由您设置的订单ID来识别您的订单
        /// </summary>
        public String client_oidBack { get; set; }
        /// <summary>
        /// 下单结果。若是下单失败，将给出错误码提示
        /// </summary>
        public Boolean result { get; set; }
        /// <summary>
        /// 错误码，下单成功时为0，下单失败时会显示相应错误码
        /// </summary>
        public String error_code { get; set; }
        /// <summary>
        /// 错误信息，下单成功时为空，下单失败时会显示错误信息
        /// </summary>
        public String error_message { get; set; }
    }
}
