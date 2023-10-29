using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi_cake_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;
        public OrderStatusesController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatuses()
        {
            var orderStatusesResponseDto = await _orderStatusService.GetAllOrderStatuses();

            return Ok(new ResponseDto()
            {
                Data = orderStatusesResponseDto
            });
        }
    }
}
