
namespace dotnetcoreapi.cake.shop.domain
{
    public interface IOrderStatusRepository
    {
        // Get all order statuses
        IQueryable<OrderStatus> GetAllOrderStatuses();

        // Get order status by id
        Task<OrderStatus> GetOrderStatusById(int orderStatusId);

        // Get order status by status
        Task<OrderStatus> GetOrderStatusByStatus(string status);

        // Create order status
        Task<OrderStatus> CreateOrderStatus(OrderStatus orderStatus);

        // Delete order status
        Task<OrderStatus> DeleteOrderStatus(OrderStatus orderStatus);
    }
}
