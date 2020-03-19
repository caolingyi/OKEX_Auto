using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKEX.Auto.TradeApi
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectSettings
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RedisConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RedisDb { get; set; }
    }
}
