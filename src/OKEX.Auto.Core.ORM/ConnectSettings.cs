using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.ORM
{
    public class ConnectSettings
    {
        public string ConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
        public int RedisDb { get; set; }
        public string MongoConnectionString { get; set; }
        public string MongoDb { get; set; }
    }
}
