using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.Models.Okex.Account
{
    /// <summary>
    /// 提币手续费
    /// </summary>
    public class OkexAccountWithDrawalFee
    {
        /// <summary>
        /// 币种
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 最小提币手续费数量
        /// </summary>
        public decimal min_fee { get; set; }
        /// <summary>
        ///  最大提币手续费数量
        /// </summary>
        public decimal max_fee { get; set; }
    }
}
