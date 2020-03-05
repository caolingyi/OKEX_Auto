using System;
using System.Collections.Generic;
using System.Linq;

namespace OKEX.Auto.Core.Domain.SeedWork
{
    public class PageResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<T> Value { get; set; }
    }

    public class PageParam
    {
        public int PageIndex
        {
            get;
            set;
        } = 0;
        /// <summary>
        /// 每页显示的记录数（小于等于0时表示不分页）。
        /// </summary>
        public int PageSize
        {
            get;
            set;
        } = 10;
        /// <summary>
        /// 符合条件的总记录数。
        /// </summary>
        public int TotalCount
        {
            get;
            set;
        }
    }
}
