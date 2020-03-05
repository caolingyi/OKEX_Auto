using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel
{
    public class BIBIAccountAsset
    {
        /// <summary>
        /// 账户id
        /// </summary>
        public string accountId { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public String currency { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public String balance { get; set; }
        /// <summary>
        /// 冻结（不可用）
        /// </summary>
        public String hold { get; set; }
        /// <summary>
        /// 可用于交易的数量
        /// </summary>
        public String available { get; set; }

        public String frozen { get; set; }

        public String holds { get; set; }
    }
}
