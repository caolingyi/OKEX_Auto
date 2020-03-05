using System.Collections.Generic;

namespace OKEX.Auto.Core.Utilities
{
    /// <summary>
    /// layUI2.0列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LayGrid<T> where T : class
    {
        /// <summary>
        /// 获取结果。
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 备注信息。
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 数据列表。
        /// </summary>
        public List<T> data { get; set; }

        /// <summary>
        /// 总页数。
        /// </summary>
        public int count { get; set; }
    }

    public class BootstrapGrid<T> where T : class
    {
        /// <summary>
        /// 获取结果。
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 备注信息。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据列表。含总数
        /// </summary>
        public BootstrapData<T> Data { get; set; }
    }

    public class BootstrapData<T> where T : class
    {
        
        public int total { get; set; }
        /// <summary>
        /// 数据列表。
        /// </summary>
        public List<T> rows { get; set; }
    }
}