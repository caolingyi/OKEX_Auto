using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain
{
   public class BIBIBillFlow
    {
        /// <summary>
        /// 账单ID
        /// </summary>
        public string ledger_id { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 变动数量
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 流水来源 transfer	资金转入/转出 trade 交易产生的资金变动 rebate 返佣
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 账单创建时间
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 如果type是trade或者fee，则会有该details字段将包含order，instrument信息
        /// </summary>
        public string details { get; set; }
        /// <summary>
        /// 交易的ID
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        ///  交易的币对
        /// </summary>
        public string instrument_id { get; set; }
    }
}
