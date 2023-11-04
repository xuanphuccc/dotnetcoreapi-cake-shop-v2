using dotnetcoreapi.cake.shop.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi.cake.shop
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> FilterAsync(
            [FromQuery] int? status,
            [FromQuery] int? pageSize,
            [FromQuery] int? page,
            [FromQuery] string? sort,
            [FromQuery] string? search)
        {
            var allOrderResponseDtos = await _orderService.FilterAsync(status, pageSize, page, sort, search);

            return Ok(allOrderResponseDtos);
        }

        /// <summary>
        /// Lấy đơn hàng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int? id)
        {
            var orderResponseDto = await _orderService.GetEntityByIdAsync(id.Value);

            return Ok(new ResponseDto() { Data = orderResponseDto });
        }

        /// <summary>
        /// Thêm mới đơn hàng
        /// </summary>
        /// <param name="orderRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto orderRequestDto)
        {
            var result = await _orderService.CreateEntityAsync(orderRequestDto);

            return StatusCode(StatusCodes.Status201Created, new ResponseDto() { Data = result });
        }

        /// <summary>
        /// Chuyển trạng thái sang Đang giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("delivery/{id}")]
        public async Task<IActionResult> DeliveryOrder([FromRoute] int id)
        {
            var result = await _orderService.DeliveryOrder(id);

            return Ok(new ResponseDto() { Data = result });
        }

        /// <summary>
        /// Chuyển trạng thái sang Đã hoàn thành
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("complete/{id}")]
        public async Task<IActionResult> CompleteOrder([FromRoute] int id)
        {
            var result = await _orderService.SuccessOrder(id);

            return Ok(new ResponseDto() { Data = result });
        }

        /// <summary>
        /// Chuyển trạng thái sang Đã huỷ đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelOrder([FromRoute] int id)
        {
            var result = await _orderService.CancelOrder(id);

            return Ok(new ResponseDto() { Data = result });
        }
    }
}
