using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.Models.Okex
{
    public class OkexBaseReponse
    {
        /// <summary>
        ///  错误码，下单成功时为0，下单失败时会显示相应错误码
        /// </summary>
        public string error_code { get; set; }
        /// <summary>
        /// 错误信息，下单成功时为空，下单失败时会显示错误信息
        /// </summary>
        public int error_message { get; set; }
        /// <summary>
        /// 调用接口返回结果
        /// </summary>
        public bool result { get; set; }
    }
}