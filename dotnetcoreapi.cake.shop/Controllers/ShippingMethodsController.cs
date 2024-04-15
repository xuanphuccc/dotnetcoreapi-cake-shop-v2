using dotnetcoreapi.cake.shop.application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcoreapi.cake.shop
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingMethodsController : BaseController<ShippingMethodDto, ShippingMethodRequestDto, ShippingMethodRequestDto>
    {
        private readonly IShippingMethodService _shippingMethodService;
        public ShippingMethodsController(IShippingMethodService shippingMethodService) : base(shippingMethodService)
        {
            _shippingMethodService = shippingMethodService;
        }

        /// <summary>
        /// Lấy đơn vị vận chuyển mặc định
        /// </summary>
        /// <returns></returns>
        [HttpGet("default")]
        public async Task<IActionResult> GetDefaultShippingMethod()
        {
            var shippingMethodResponseDto = await _shippingMethodService.GetDefaultShippingMethod();

            return Ok(new ResponseDto() { Data = shippingMethodResponseDto });
        }
    }
}
