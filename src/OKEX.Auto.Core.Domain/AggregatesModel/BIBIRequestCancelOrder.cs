using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Domain.AggregatesModel
{
   public class BIBIRequestCancelOrder
    {
        /// <summary>
        /// 提供此参数则撤销指定币对的相应订单，如果不提供此参数则返回错误码
        /// </summary>
        public String instrument_id { get; set; }
        /// <summary>
        /// order_id和client_oid必须且只能选一个填写，由您设置的订单ID来识别您的订单 , 类型为字母（大小写）+数字或者纯字母（大小写）1-32位字符
        /// </summary>
        public String client_oid { get; set; }
        /// <summary>
        /// order_id和client_oid必须且只能选一个填写，订单ID
        /// </summary>
        public String order_id { get; set; }


        /// <summary>
        ///  订单ID
        /// </summary>
        public String order_idBack { get; set; }
        /// <summary>
        /// 由您设置的订单ID来识别您的订单
        /// </summary>
        public String client_oidBack { get; set; }
        /// <summary>
        /// 撤单申请结果。若是撤单失败，将给出错误码提示
        /// </summary>
        public Boolean result { get; set; }
        /// <summary>
        /// 错误码，撤单成功时为0，撤单失败时会显示相应错误码
        /// </summary>
        public String error_code { get; set; }
        /// <summary>
        /// 错误信息，撤单成功时为空，撤单失败时会显示错误信息
        /// </summary>
        public String error_message { get; set; }
    }
}
