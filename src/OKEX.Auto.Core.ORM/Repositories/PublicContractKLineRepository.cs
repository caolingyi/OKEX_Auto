using MongoDB.Bson;
using MongoDB.Driver;
using OKEX.Auto.Core.Domain.AggregatesModel.contract;
using OKEX.Auto.Core.Domain.Interface;
using OKEX.Auto.Core.ORM.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.ORM.Repositories
{
    public class PublicContractKLineRepository : IPublicContractKLineRepository
    {
        private readonly ContractDataContext _context;

        public PublicContractKLineRepository(ConnectSettings connectSettings)
        {
            _context = new ContractDataContext(connectSettings);
        }

        public async Task<PublicContractKLine> GetAsync(string id)
        {
            var filter = Builders<PublicContractKLine>.Filter.Eq("Id", id);
            return await _context.PublicContractKLines
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }
        public async Task<List<PublicContractKLine>> GetListAsync()
        {
            return await _context.PublicContractKLines.Find(new BsonDocument()).ToListAsync();
        }


        public async Task AddUserLocationAsync(PublicContractKLine publicContractKLine)
        {
            await _context.PublicContractKLines.InsertOneAsync(publicContractKLine);
        }

        public async Task UpdateUserLocationAsync(PublicContractKLine publicContractKLine)
        {
            await _context.PublicContractKLines.ReplaceOneAsync(
                doc => doc.Id == publicContractKLine.Id,
                publicContractKLine,
                new UpdateOptions { IsUpsert = true });
        }
    }
}
