using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain
{
    public class BaseCapitalAccountEstimate
    {
        /// <summary>
        /// 按照某一个法币为单位的估值，如BTC
        /// </summary>
        public String valuation_currency { get; set; }
        /// <summary>
        /// 预估资产
        /// </summary>
        public String balance { get; set; }
        /// <summary>
        /// 数据返回时间
        /// </summary>
        public String timestamp { get; set; }
        /// <summary>
        /// 账户类型
        /// </summary>
        public String account_type { get; set; }
    }

//    获取某一个业务线资产估值。0.预估总资产
//1.币币账户
//3.交割账户
//4.法币账户
//5.币币杠杆
//6.资金账户
//8. 余币宝账户
//9.永续合约
//12.期权 
//14.挖矿账户 
//15.交割usdt保证金账户
//16.永续usdt保证金账户
//默认为0，查询总资产
}
