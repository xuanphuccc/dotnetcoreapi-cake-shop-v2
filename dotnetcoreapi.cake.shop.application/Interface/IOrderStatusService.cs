
namespace dotnetcoreapi.cake.shop.application
{
    public interface IOrderStatusService
    {
        // Get all order statuses response DTO
        Task<List<OrderStatusResponseDto>> GetAllOrderStatuses();
    }
}
