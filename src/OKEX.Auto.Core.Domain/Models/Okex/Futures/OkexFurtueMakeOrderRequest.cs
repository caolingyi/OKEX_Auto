using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.Models.Okex.Futures
{
    /// <summary>
    /// 交割合约 下单 请求参数
    /// </summary>
    public class OkexFurtueMakeOrderRequest
    {
        /// <summary>
        /// 	合约ID，如BTC-USD-180213 ,BTC-USDT-191227
        /// </summary>
        public string instrument_id { get; set; }
        /// <summary>
        /// 类型  1:开多 2:开空 3:平多 4:平空
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 类型  
        /// 0：普通委托（order type不填或填0都是普通委托）
        /// 1：只做Maker（Post only）
        /// 2：全部成交或立即取消（FOK）
        /// 3：立即成交并取消剩余（IOC）
        /// </summary>
        public string order_type { get; set; }

        /// <summary>
        /// 委托价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 	买入或卖出合约的数量（以张计数）
        /// </summary>
        public int size { get; set; }
        //public int leverage  { get; set; }
        /// <summary>
        /// 由您设置的订单ID来识别您的订单,格式是字母（区分大小写）+数字 或者 纯字母（区分大小写），1-32位字符 （不能重复）
        /// </summary>
        public string client_oid { get; set; }
        /// <summary>
        /// 是否以对手价下单(0:不是; 1:是)，默认为0，当取值为1时，price字段无效。当以对手价下单，order_type只能选择0（普通委托）
        /// </summary>
        public string match_price { get; set; }
    }
}
