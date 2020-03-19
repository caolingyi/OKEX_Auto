using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKEX.Auto.TradeAdmin
{
    public class ConnectSettings
    {
        public string ConnectionString { get; set; }

        public string RedisConnectionString { get; set; }
        public int RedisDb { get; set; }
    }
}
