
namespace dotnetcoreapi.cake.shop.domain
{
    public interface IOrderRepository
    {
        // Get all orders
        IQueryable<Order> GetAllOrders();

        // Get order by id
        Task<Order> GetOrderById(int orderId);

        // Check product has orders or not
        Task<int> HasOrders(int productId);

        // Create order
        Task<Order> CreateOrder(Order order);

        // Update order
        Task<Order> UpdateOrder(Order order);

        // Update order
        Task<Order> DeleteOrder(Order order);
    }
}
