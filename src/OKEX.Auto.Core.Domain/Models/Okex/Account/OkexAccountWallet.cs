using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.Models.Okex.Account
{
    /// <summary>
    /// 账户钱包信息
    /// </summary>
    public  class OkexAccountWallet
    {
        /// <summary>
        /// 币种，如BTC
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 	余额
        /// </summary>
        public decimal balance { get; set; }
        /// <summary>
        /// 	冻结（不可用）
        /// </summary>
        public decimal hold { get; set; }
        /// <summary>
        /// 可用余额
        /// </summary>
        public decimal available { get; set; }
    }
}
