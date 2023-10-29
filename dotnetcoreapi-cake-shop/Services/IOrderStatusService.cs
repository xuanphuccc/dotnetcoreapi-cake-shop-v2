using dotnetcoreapi_cake_shop.Dtos;

namespace dotnetcoreapi_cake_shop.Services
{
    public interface IOrderStatusService
    {
        // Get all order statuses response DTO
        Task<List<OrderStatusResponseDto>> GetAllOrderStatuses();
    }
}
