using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.domain
{
    public interface IReportRepository
    {
        /// <summary>
        /// Lấy tổng số đơn hàng đã bán
        /// </summary>
        /// <returns></returns>
        Task<int> GetTotalOrders();

        /// <summary>
        /// Lấy tổng doanh thu
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetTotalRevenue();

        /// <summary>
        /// Lấy tổng số khách hàng
        /// </summary>
        /// <returns></returns>
        Task<int> GetTotalCustomers();
    }
}
