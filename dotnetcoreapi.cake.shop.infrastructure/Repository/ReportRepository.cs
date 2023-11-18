using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class ReportRepository : IReportRepository
    {
        private readonly CakeShopContext _context;

        public ReportRepository(CakeShopContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy tổng số đơn hàng đã bán
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetTotalOrders() {
            var totalOrders = await _context.Orders.CountAsync(or => or.OrderStatusId != (int)EOrderStatus.Cancelled);

            return totalOrders;
        }

        /// <summary>
        /// Lấy tổng doanh thu
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> GetTotalRevenue() {
            var allOrders = await _context.Orders.ToListAsync();

            decimal totalRevenue = 0;
            allOrders.ForEach(order => {
                if(order.OrderStatusId == (int)EOrderStatus.Completed)
                {
                    totalRevenue += order.OrderTotal;
                }
            });

            totalRevenue = Math.Round(totalRevenue / 1000000, 2);

            return totalRevenue;
        }

        /// <summary>
        /// Lấy tổng số khách hàng
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetTotalCustomers() {
            var allOrders = await _context.Orders.ToListAsync();

            var customerPhoneNumbers = new List<string>();
            allOrders.ForEach(order => {
                if(!customerPhoneNumbers.Contains(order.CustomerPhoneNumber))
                {
                    customerPhoneNumbers.Add(order.CustomerPhoneNumber);
                }
            });

            var totalCustomers = customerPhoneNumbers.Count();

            return totalCustomers;
        }
    }
}
