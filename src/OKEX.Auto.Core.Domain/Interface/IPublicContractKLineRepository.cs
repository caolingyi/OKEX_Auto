using OKEX.Auto.Core.Domain.AggregatesModel.contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.Domain.Interface
{
    public interface IPublicContractKLineRepository
    {
        Task<PublicContractKLine> GetAsync(string id);
        Task<List<PublicContractKLine>> GetListAsync();
        Task AddAsync(PublicContractKLine publicContractKLine);
        Task UpdateAsync(PublicContractKLine publicContractKLine);
        Task AddRangeAsync(List<PublicContractKLine> publicContractKLines);
    }
}
