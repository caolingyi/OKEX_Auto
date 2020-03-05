using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel
{
    class BIBIAccountFeeRate
    {
        /// <summary>
        /// 吃单手续费率
        /// </summary>
        public string taker { get; set; }
        /// <summary>
        /// 挂单手续费率
        /// </summary>
        public string maker { get; set; }
        /// <summary>
        /// 数据返回时间
        /// </summary>
        public string timestamp { get; set; }
    }
}
