using OKEX.Auto.Core.Domain.AggregatesModel;
using OKEX.Auto.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.Domain.Interface
{
    public interface IBaseCurrencyRepository : IRepository<BaseCurrency>
    {
        Task<PageResult<BaseCurrency>> ListPaged();
    }
}
