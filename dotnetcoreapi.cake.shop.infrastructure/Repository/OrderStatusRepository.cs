using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly CakeShopContext _context;
        public OrderStatusRepository(CakeShopContext context)
        {
            _context = context;
        }

        // Get all order statuses
        public IQueryable<OrderStatus> GetAllOrderStatuses()
        {
            var orderStatuses = _context.OrderStatuses.AsQueryable();

            return orderStatuses;
        }

        // Get order status by id
        public async Task<OrderStatus> GetOrderStatusById(int orderStatusId)
        {
            var orderStatus = await _context.OrderStatuses
                                    .FirstOrDefaultAsync(o => o.OrderStatusId == orderStatusId);

            return orderStatus!;
        }

        // Get order status by status
        public async Task<OrderStatus> GetOrderStatusByStatus(string status)
        {
            var orderStatus = await _context.OrderStatuses
                                    .FirstOrDefaultAsync(o => o.Status.ToLower() == status.ToLower());

            return orderStatus!;

        }

        // Create order status
        public async Task<OrderStatus> CreateOrderStatus(OrderStatus orderStatus)
        {
            await _context.OrderStatuses.AddAsync(orderStatus);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("cannot create order status");
            }

            return orderStatus;
        }

        // Delete order status
        public async Task<OrderStatus> DeleteOrderStatus(OrderStatus orderStatus)
        {
            _context.OrderStatuses.Remove(orderStatus);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("cannot delete order status");
            }

            return orderStatus;
        }
    }
}
