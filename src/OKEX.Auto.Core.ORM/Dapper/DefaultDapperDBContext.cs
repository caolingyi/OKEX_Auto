using Microsoft.Extensions.Options;
using Npgsql;
using OKEX.Auto.Core.ORM.Dapper.Base;
using System.Data;
using System.Data.SqlClient;

namespace OKEX.Auto.Core.Context
{
    public class DefaultDapperDBContext : DapperDBContext
    {
        public DefaultDapperDBContext(IOptions<DapperDBContextOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        protected override IDbConnection CreateConnection(string connectionString, string dbType)
        {
            if (dbType == "PostgreSql")
            {
                IDbConnection conn = new NpgsqlConnection(connectionString);
                return conn;
            }
            else
            {
                IDbConnection conn = new SqlConnection(connectionString);
                return conn;
            }
        }
    }


}
