using System;
using System.ComponentModel;

namespace Asmkt.SupplierHandle.Core.DomainCore
{
    public abstract class BaseEntity
    {
        int? _requestedHashCode;

        private long _id = -1;

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public StatusEnum Status { get; set; }

        public DateTime? CreatedTime { get; set; }

        public bool IsTransient()
        {
            return this.Id == default(Int64);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseEntity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            BaseEntity item = (BaseEntity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }

        public virtual void Init(long id = 0)
        {
            //this.Id = id > 0 ? id : IdHelper.BuildId();
            this.Status = StatusEnum.Enable;
            this.CreatedTime = DateTime.Now;
        }

        public virtual void Enable()
        {
            this.Status = StatusEnum.Enable;
        }

        public virtual void Disable()
        {
            this.Status = StatusEnum.Disable;
        }
    }

    public enum StatusEnum
    {
        [Description("禁用")]
        Disable = -10,
        [Description("启用")]
        Enable = 10,
    }


    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 作废
        /// </summary>
        [Description("作废")]
        IsDelete = -10,
        /// <summary>
        /// 新建
        /// </summary>
        [Description("新建")]
        New = 0,
        /// <summary>
        /// 已支付
        /// </summary>
        [Description("已支付")]
        Payed = 10,
        /// <summary>
        /// 已中奖
        /// </summary>
        [Description("已中奖")]
        IsGet = 20,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Finished = 30,
        /// <summary>
        /// 已结算
        /// </summary>
        [Description("已结算")]
        IsSettlement = 40,
    }
}
