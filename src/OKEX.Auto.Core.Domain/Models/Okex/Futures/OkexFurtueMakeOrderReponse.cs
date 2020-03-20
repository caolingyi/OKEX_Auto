using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.Models.Okex.Futures
{
    /// <summary>
    /// 交割合约 下单 返回数据
    /// </summary>
    public class OkexFurtueMakeOrderReponse:OkexBaseReponse
    {
        /// <summary>
        /// 订单ID，下单失败时，此字段值为-1
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 由您设置的订单ID来识别您的订单
        /// </summary>
        public string client_oid { get; set; }

    }
}
