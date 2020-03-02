using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain
{
    public class BaseCurrency
    {
        /// <summary>
        /// 币种名称，如btc
        /// </summary>
        public String currency { get; set; }
        /// <summary>
        /// 币种中文名称，不显示则无对应名称
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// 是否可充值，0表示不可充值，1表示可以充值
        /// </summary>
        public String can_deposit { get; set; }
        /// <summary>
        /// 是否可提币，0表示不可提币，1表示可以提币
        /// </summary>
        public String can_withdraw { get; set; }
        /// <summary>
        /// 币种最小提币量
        /// </summary>
        public String min_withdrawal { get; set; }
    }
}
