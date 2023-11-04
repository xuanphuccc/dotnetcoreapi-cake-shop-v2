using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.domain
{
    public enum EOrderStatus
    {
        /// <summary>
        /// Đã tạo đơn hàng
        /// </summary>
        Created = 1,

        /// <summary>
        /// Đang vận chuyển
        /// </summary>
        Delivery = 2,

        /// <summary>
        /// Đã hoàn thành
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Đã huỷ đơn hàng
        /// </summary>
        Cancelled = 4,
    }
}
