
namespace dotnetcoreapi.cake.shop.application
{
    public interface IOrderService : IBaseService<OrderDto, OrderRequestDto, OrderRequestDto>
    {
        
        /// <summary>
        /// Tìm kiếm, phân trang, sắp xếp
        /// </summary>
        /// <param name="status">Trạng thái đơn hàng</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="page">Trang hiện tại</param>
        /// <param name="sort">Kiểu sắp xếp</param>
        /// <param name="search">Tìm kiếm</param>
        /// <returns></returns>
        Task<ResponseDto> FilterAsync(
            int? status = null,
            int? pageSize = null,
            int? page = null,
            string? sort = null,
            string? search = null);

        
        /// <summary>
        /// Chuyển trạng thái sang Đang vận chuyển
        /// </summary>
        /// <param name="orderId">ID đơn hàng</param>
        /// <returns></returns>
        Task<OrderDto> DeliveryOrder(int orderId);

        /// <summary>
        /// Chuyển trạng thái sang Đã huỷ đơn hàng
        /// </summary>
        /// <param name="orderId">ID đơn hàng</param>
        /// <returns></returns>
        Task<OrderDto> CancelOrder(int orderId);

        /// <summary>
        /// Chuyển trạng thái sang Đã hoàn thành
        /// </summary>
        /// <param name="orderId">ID đơn hàng</param>
        /// <returns></returns>
        Task<OrderDto> SuccessOrder(int orderId);
    }
}
