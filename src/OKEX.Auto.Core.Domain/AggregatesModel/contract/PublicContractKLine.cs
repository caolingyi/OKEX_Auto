using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel.contract
{
    public class PublicContractKLine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// 合约ID，如BTC-USD-180213,BTC-USDT-191227
        /// </summary>
        public String instrument_id { get; set; }

        /// <summary>
        /// 开始时间（ISO 8601标准，例如：2018-06-20T02:31:00Z）
        /// </summary>
        public String start { get; set; }

        /// <summary>
        /// 结束时间（ISO 8601标准，例如：2018-06-20T02:31:00Z）
        /// </summary>
        public String end { get; set; }

        /// <summary>
        /// 时间粒度，以秒为单位，默认值60。如[60/180/300/900/1800/3600/7200/14400/21600/43200/86400/604800]，详见下解释说明
        /// </summary>
        public String granularity { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public String timestamp { get; set; }

        /// <summary>
        /// 开盘价格
        /// </summary>
        public String open { get; set; }
        /// <summary>
        /// 最高价格
        /// </summary>
        public String high { get; set; }
        /// <summary>
        /// 最低价格
        /// </summary>
        public String low { get; set; }
        /// <summary>
        /// 收盘价格
        /// </summary>
        public String close { get; set; }
        /// <summary>
        /// 交易量（张）
        /// </summary>
        public String volume { get; set; }
        /// <summary>
        /// 按币种折算的交易量
        /// </summary>
        public String currency_volume { get; set; }
    }
}
