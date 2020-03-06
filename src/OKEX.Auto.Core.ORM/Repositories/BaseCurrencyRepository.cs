using Dapper;
using OKEX.Auto.Core.Context;
using OKEX.Auto.Core.Domain.AggregatesModel;
using OKEX.Auto.Core.Domain.Interface;
using OKEX.Auto.Core.Domain.SeedWork;
using OKEX.Auto.Core.ORM.BaseEFRepository;
using OKEX.Auto.Core.ORM.Dapper.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEX.Auto.Core.ORM.Repositories
{
   public class BaseCurrencyRepository : EFRepository<BaseCurrency>, IBaseCurrencyRepository
    {
        private readonly DapperDBContext _dapperContext;
        private readonly DefaultEFDBContext _context;
        public BaseCurrencyRepository(DefaultEFDBContext context, DapperDBContext dapperCcontext)
            : base(context)
        {
            _context = context;
            _dapperContext = dapperCcontext;
        }


        public async Task<PageResult<BaseCurrency>> ListPaged()
        {
            //StringBuilder strSql = new StringBuilder();
            //var parameters = new DynamicParameters();

            //var rowStart = (dto.PageIndex - 1) * dto.PageSize + 1;
            //var rowEnd = dto.PageIndex * dto.PageSize;
            //var condition = dto.Condition;
            //var sqlCount = "SELECT COUNT(1) FROM SupplierOrderVerificationDetail (NOLOCK) Where 1 = 1 ";
            //strSql.Append(";WITH T1 AS ( ");
            //strSql.Append(" SELECT * FROM SupplierOrderVerificationDetail (NOLOCK) WHERE 1 = 1 ");

            //if (condition.SupplierOrderVerificationId > 0)
            //{
            //    strSql.Append(" AND SupplierOrderVerificationId = @SupplierOrderVerificationId");
            //    sqlCount += " AND SupplierOrderVerificationId = @SupplierOrderVerificationId";
            //    parameters.Add("SupplierOrderVerificationId", condition.SupplierOrderVerificationId);
            //}

            //strSql.Append(" ),T2 AS(SELECT *, ROW_NUMBER() OVER (ORDER BY CreatedTime DESC) AS RowNumber from T1 ");
            //strSql.Append(" ) ");
            //strSql.Append(" SELECT * from T2 WHERE RowNumber BETWEEN @RowStart and @RowEnd ");

            //var totalCount = await _dapperContext.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);

            //parameters.Add("RowStart", rowStart);
            //parameters.Add("RowEnd", rowEnd);
            //var list = await _dapperContext.QueryAsync<BaseCurrency>(strSql.ToString(), parameters);
            //return new PageResult<BaseCurrency>
            //{
            //    Value = list,
            //    PageIndex = dto.PageIndex,
            //    PageSize = dto.PageSize,
            //    TotalCount = totalCount,
            //    TotalPages = totalCount % dto.PageSize == 0 ? (totalCount / dto.PageSize) : (totalCount / dto.PageSize + 1)
            //};
            return null;
        }
    }
}
