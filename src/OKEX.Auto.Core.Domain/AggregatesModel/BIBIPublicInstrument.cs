using OKEX.Auto.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel
{
    /// <summary>
    /// 公共 币对信息
    /// </summary>
    public class BIBIPublicInstrument : BaseEntity
    {
        /// <summary>
        /// 币对名称
        /// </summary>
        public String instrument_id { get; set; }
        /// <summary>
        /// 交易货币币种
        /// </summary>
        public String base_currency { get; set; }
        /// <summary>
        /// 	计价货币币种
        /// </summary>
        public String quote_currency { get; set; }
        /// <summary>
        /// 	最小交易数量
        /// </summary>
        public String min_size { get; set; }
        /// <summary>
        /// 交易货币数量精度
        /// </summary>
        public String size_increment { get; set; }
        /// <summary>
        /// 交易价格精度
        /// </summary>
        public String tick_size { get; set; }

    }
}
