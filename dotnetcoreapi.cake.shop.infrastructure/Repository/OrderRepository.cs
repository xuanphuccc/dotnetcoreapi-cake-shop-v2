using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CakeShopContext _context;
        public OrderRepository(CakeShopContext context)
        {
            _context = context;
        }

        // Get all orders
        public IQueryable<Order> GetAllOrders()
        {
            var allOrders = _context.Orders
                                .Include(o => o.ShippingMethod)
                                .Include(o => o.OrderStatus)
                                .Include(o => o.Items)
                                    .ThenInclude(item => item.Product)
                                    .ThenInclude(product => product.Images)
                                .AsQueryable();
            return allOrders;
        }

        // Get order by id
        public async Task<Order> GetOrderById(int orderId)
        {
            var order = await _context.Orders
                                .Include(o => o.ShippingMethod)
                                .Include(o => o.OrderStatus)
                                .Include(o => o.Items)
                                    .ThenInclude(item => item.Product)
                                    .ThenInclude(product => product.Images)
                                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return order!;
        }

        // Check product has orders or not
        public async Task<int> HasOrders(int productId)
        {
            var hasOrders = await _context.OrderItems.CountAsync(oi => oi.ProductId == productId);

            return hasOrders;
        }

        // Create order
        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("cannot create order");
            }

            return order;
        }

        // Update order
        public async Task<Order> UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            var reusult = await _context.SaveChangesAsync();

            if (reusult == 0)
            {
                throw new Exception("not modified");
            }

            return order;
        }

        // Delete order
        public async Task<Order> DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("cannot delete order");
            }

            return order;
        }
    }
}
