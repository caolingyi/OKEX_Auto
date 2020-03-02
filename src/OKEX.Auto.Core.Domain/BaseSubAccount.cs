using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain
{
    public class BaseSubAccount
    {
        /// <summary>
        /// 账户余额 （可用余额和冻结余额的总和）
        /// </summary>
        public String balance { get; set; }
        /// <summary>
        /// 冻结（不可用）
        /// </summary>
        public String hold { get; set; }
        /// <summary>
        /// 可用余额 --币币和资金
        /// </summary>
        public String available { get; set; }
        /// <summary>
        /// 账户权益 --交割和永续
        /// </summary>
        public String equity { get; set; }
        /// <summary>
        /// 可划转数量
        /// </summary>
        public String max_withdraw { get; set; }
        /// <summary>
        /// 币种，如BTC
        /// </summary>
        public String currency { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public String sub_account { get; set; }
        /// <summary>
        /// 以btc为单位 账户资产总估值
        /// </summary>
        public String asset_valuation { get; set; }
        /// <summary>
        /// 子账户里的 账户类型 
        /// </summary>
        public String account_type { get; set; }
    }

//    1.币币账户 
//3.交割账户 
//4.法币账户
//5.币币杠杆
//6.资金账户账户
//8. 余币宝账户
//9.永续合约账户
//12.期权账户
//14.挖矿账户
}
