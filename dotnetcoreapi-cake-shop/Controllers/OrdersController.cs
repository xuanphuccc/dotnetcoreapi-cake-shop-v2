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

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(
            [FromQuery] int? status,
            [FromQuery] int? pageSize,
            [FromQuery] int? page,
            [FromQuery] string? sort,
            [FromQuery] string? search)
        {
            try
            {
                // Get all orders
                var allOrderResponseDtos = await _orderService.GetAllOrders(status, pageSize, page, sort, search);

                return Ok(allOrderResponseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "orderId is required" });
            }

            try
            {
                // Get order
                var orderResponseDto = await _orderService.GetOrderById(id.Value);
                if (orderResponseDto == null)
                {
                    return NotFound(new ResponseDto() { Status = 404, Title = "order not found" });
                }

                return Ok(new ResponseDto()
                {
                    Data = orderResponseDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto orderRequestDto)
        {
            if (orderRequestDto == null)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "order is required" });
            }

            try
            {
                // Create order
                var createdOrderResponseDto = await _orderService.CreateOrder(orderRequestDto);

                return CreatedAtAction(
                    nameof(GetOrder),
                    new { id = createdOrderResponseDto.OrderId },
                    new ResponseDto()
                    {
                        Data = createdOrderResponseDto,
                        Status = 201,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpPut("delivery/{id}")]
        public async Task<IActionResult> DeliveryOrder([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "orderId is required" });
            }

            try
            {
                // Delivery order
                var updatedOrderResponseDto = await _orderService.DeliveryOrder(id.Value);

                return Ok(new ResponseDto()
                {
                    Data = updatedOrderResponseDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpPut("complete/{id}")]
        public async Task<IActionResult> CompleteOrder([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "orderId is required" });
            }

            try
            {
                // Complete order
                var updatedOrderResponseDto = await _orderService.SuccessOrder(id.Value);

                return Ok(new ResponseDto()
                {
                    Data = updatedOrderResponseDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelOrder([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ResponseDto() { Status = 400, Title = "orderId is required" });
            }

            try
            {
                // Cancel order
                var updatedOrderResponseDto = await _orderService.CancelOrder(id.Value);

                return Ok(new ResponseDto()
                {
                    Data = updatedOrderResponseDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseDto() { Status = 500, Title = ex.Message }
                );
            }
        }
    }
}
