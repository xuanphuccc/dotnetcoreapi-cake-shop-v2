
namespace dotnetcoreapi.cake.shop.application
{
    public interface IOrderService
    {
        // Get all orders response DTO
        Task<ResponseDto> GetAllOrders(
            int? status = null,
            int? pageSize = null,
            int? page = null,
            string? sort = null,
            string? search = null);

        // Get order response DTO
        Task<OrderResponseDto> GetOrderById(int orderId);

        // Create order
        Task<OrderResponseDto> CreateOrder(OrderRequestDto orderRequestDto);

        // Update order status
        Task<OrderResponseDto> DeliveryOrder(int orderId);
        Task<OrderResponseDto> CancelOrder(int orderId);
        Task<OrderResponseDto> SuccessOrder(int orderId);

        // Delete order
        Task<OrderResponseDto> DeleteOrder(int orderId);
    }
}
