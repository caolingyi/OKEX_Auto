using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel
{
    public class BaseCapitalAccount
    {
        /// <summary>
        /// 币种，如BTC
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
        /// 可用余额
        /// </summary>
        public String available { get; set; }
    }
}
