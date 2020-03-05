using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel
{
    public class BaseCapitalAccountFlow
    {
        /// <summary>
        /// 账单ID
        /// </summary>
        public String ledger_id { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public String currency { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public String balance { get; set; }
        /// <summary>
        /// 变动数量
        /// </summary>
        public String amount { get; set; }
        /// <summary>
        /// 账单类型
        /// </summary>
        public String typename { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public String fee { get; set; }
        /// <summary>
        /// 账单创建时间
        /// </summary>
        public String timestamp { get; set; }
    }
}
