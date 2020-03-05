namespace OKEX.Auto.Core.Utilities
{
    /// <summary>
    /// 通用AJAX请求响应数据格式模型。
    /// </summary>
    public class AjaxResult
    {
        public AjaxResult(ResultType code, string msg, object data = null)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }

        /// <summary>
        /// 结果类型。
        /// </summary>
        public object code { get; set; }

        /// <summary>
        /// 消息内容。
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回数据。
        /// </summary>
        public object data { get; set; }
    }

    /// <summary>
    /// 结果类型枚举。
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 成功。
        /// </summary>
        Success = 1,

        /// <summary>
        /// 异常。
        /// </summary>
        Error = -1,

        /// <summary>
        /// 警告。
        /// </summary>
        Warning = 0
    }
}