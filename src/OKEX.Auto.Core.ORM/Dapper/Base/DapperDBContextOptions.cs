using Microsoft.Extensions.Options;

namespace OKEX.Auto.Core.ORM.Dapper.Base
{
    public class DapperDBContextOptions : IOptions<DapperDBContextOptions>
    {
        public string Configuration { get; set; }

        public string DbType { get; set; }

        DapperDBContextOptions IOptions<DapperDBContextOptions>.Value
        {
            get { return this; }
        }
    }
}
