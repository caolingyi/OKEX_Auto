using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OKEX.Auto.Core.Domain.AggregatesModel.contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.ORM.Mongo
{
   public class ContractDataContext
    {
        private readonly IMongoDatabase _database = null;

        public ContractDataContext(ConnectSettings connectSettings)
        {
            var client = new MongoClient(connectSettings.MongoConnectionString);
            if (client != null)
                _database = client.GetDatabase(connectSettings.MongoDb);
        }

        public IMongoCollection<PublicContractKLine> PublicContractKLines
        {
            get
            {
                return _database.GetCollection<PublicContractKLine>("KLine");
            }
        }
    }
}
